using System;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class GameHandler : IDisposable
    {
        [Inject] private readonly StageStore _stageStore;
        [Inject] private readonly EntityStore<Goal, GoalFactory.FactoryParams> _goalStore;
        [Inject] private readonly EntityStore<Unit, UnitFactory.FactoryParams> _unitStore;
        [Inject] private readonly EntityStore<Bullet, BulletFactory.FactoryParams> _bulletStore;
        [Inject] private readonly EntityStore<Enemy, EnemyFactory.FactoryParams> _enemyStore;
        [Inject] private readonly EntityStore<Pedestal, PedestalFactory.FactoryParams> _pedestalStore;
        [Inject] private readonly UnitStateUpdater _unitStateUpdater;
        [Inject] private readonly BulletStateUpdater _bulletStateUpdater;
        [Inject] private readonly EnemyStateUpdater _enemyStateUpdater;
        [Inject] private readonly StageScheduler _stageScheduler;
        [Inject] private readonly IPublisher<GameStartingEvent> _gameStartingPub;
        [Inject] private readonly IPublisher<GameFailedEvent> _gameFailedPub;
        [Inject] private readonly IPublisher<GameClearedEvent> _gameClearedPub;
        [Inject] private readonly IPublisher<WaveStartingEvent> _waveStartingPub;
        [Inject] private readonly IPublisher<WaveClearedEvent> _waveClearedPub;
        [Inject] private readonly ISubscriber<ModelLoopEvent> _modelLoopSub;
        [Inject] private readonly ISubscriber<UnitPlacingEvent> _unitPlacingSub;
        [Inject] private readonly ISubscriber<BulletPlacingEvent> _bulletPlacingSub;
        [Inject] private readonly ISubscriber<BulletHitEvent> _bulletHitSub;
        [Inject] private readonly ISubscriber<EnemyScheduledEvent> _enemyScheduledSub;
        [Inject] private readonly ISubscriber<EnemyGoaledEvent> _enemyGoaledSub;
        [Inject] private readonly ISubscriber<GameStartedEvent> _gameStartedSub;
        [Inject] private readonly ISubscriber<GameFailedEvent> _gameFailedSub;
        [Inject] private readonly ISubscriber<GameClearingEvent> _gameClearingSub;
        [Inject] private readonly ISubscriber<GameClearedEvent> _gameClearedSub;
        [Inject] private readonly ISubscriber<WaveStartedEvent> _waveStartedSub;
        [Inject] private readonly ISubscriber<WaveFailedEvent> _waveFailedSub;
        [Inject] private readonly ISubscriber<WaveClearedEvent> _waveClearedSub;

        public bool Finished { get; private set; } = true;

        private StageNumber _currentStage;
        private WaveNumber _currentWave;
        private int _currentUnitId = Entity<Unit>.None.Id;
        private int _currentBulletId = Entity<Bullet>.None.Id;

        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public void Init()
        {
            _modelLoopSub.Subscribe(OnModelLoop).AddTo(_disposables);
            _unitPlacingSub.Subscribe(OnUnitPlacing).AddTo(_disposables);
            _bulletPlacingSub.Subscribe(OnBulletPlacing).AddTo(_disposables);
            _bulletHitSub.Subscribe(OnBulletHit).AddTo(_disposables);
            _enemyScheduledSub.Subscribe(OnEnemyScheduled).AddTo(_disposables);
            _enemyGoaledSub.Subscribe(OnEnemyGoaled).AddTo(_disposables);
            _gameStartedSub.Subscribe(OnGameStarted).AddTo(_disposables);
            _gameFailedSub.Subscribe(OnGameFailed).AddTo(_disposables);
            _gameClearingSub.Subscribe(OnGameClearing).AddTo(_disposables);
            _gameClearedSub.Subscribe(OnGameCleared).AddTo(_disposables);
            _waveStartedSub.Subscribe(OnWaveStarted).AddTo(_disposables);
            _waveFailedSub.Subscribe(OnWaveFailed).AddTo(_disposables);
            _waveClearedSub.Subscribe(OnWaveCleared).AddTo(_disposables);
        }

        private void OnModelLoop(ModelLoopEvent ev)
        {
            UnityEngine.Debug.Log("GameHandler.OnModelLoop");

            if (_currentStage.Equals(default(StageNumber)) && _currentWave.Equals(default(WaveNumber)))
            {
                _gameStartingPub.Publish(new GameStartingEvent());
                return;
            }

            _enemyStore.ModifyAll(enemy => {
                UpdateEnemy(ev, ref enemy);
                return enemy;
            });

            _unitStore.ModifyAll(unit => {
                UpdateUnit(ev, ref unit);
                return unit;
            });

            _bulletStore.ModifyAll(bullet => {
                UpdateBullet(ev, ref bullet);
                return bullet;
            });

            _enemyStore.DestroyAll(enemy => !enemy.IsAlive);
            _unitStore.DestroyAll(unit => !unit.IsAlive);
            _bulletStore.DestroyAll(bullet => !bullet.IsAlive);

            if (!Finished && _stageScheduler.Finished && _enemyStore.Count == 0)
            {
                _waveClearedPub.Publish(new WaveClearedEvent());
            }
        }

        private void UpdateEnemy(ModelLoopEvent ev, ref Entity<Enemy> enemy)
        {
            _enemyStateUpdater.UpdateToNext(ref enemy, _stageStore.Stage.Route, ev.DeltaTime);
        }

        private void UpdateUnit(ModelLoopEvent ev, ref Entity<Unit> unit)
        {
            _unitStateUpdater.UpdateToNext(ref unit, ev.DeltaTime);
        }

        private void UpdateBullet(ModelLoopEvent ev, ref Entity<Bullet> bullet)
        {
            _bulletStateUpdater.UpdateToNext(ref bullet, ev.DeltaTime);
        }

        private void OnUnitPlacing(UnitPlacingEvent ev)
        {
            var unit = _unitStore.Add(new(_currentUnitId, (UnitSetting.KindType)ev.Kind, ev.Position));
            _currentUnitId++;

            if (_pedestalStore.TryGet(ev.PedestalId, out var pedestal))
            {
                unit.Data.PedestalId = pedestal.Id;
                pedestal.Data.UnitId = unit.Id;
            }
        }

        private void OnBulletPlacing(BulletPlacingEvent ev)
        {
            var bullet = _bulletStore.Add(new(_currentBulletId, (BulletSetting.KindType)ev.Kind, ev.Position, ev.TargetId, ev.Attack));
            _currentBulletId++;
        }

        private void OnBulletHit(BulletHitEvent ev)
        {
            _bulletStore.TryModify(ev.BulletId, bullet =>
            {
                bullet.IsAlive = false;
                return bullet;
            },
            out var bullet);

            _enemyStore.TryModify(ev.TargetId, enemy =>
            {
                enemy.Data.Health -= bullet.Data.Attack;
                if (enemy.Data.Health <= 0.0F)
                {
                    enemy.IsAlive = false;
                }
                return enemy;
            },
            out _);
        }

        private void OnEnemyScheduled(EnemyScheduledEvent ev)
        {
            _enemyStore.Add
            (
                new EnemyFactory.FactoryParams
                (
                    ev.ReservedEnemy.Id,
                    (EnemySetting.KindType)ev.ReservedEnemy.Kind,
                    ev.ReservedEnemy.OffsetFactor
                )
            );
        }

        private void OnEnemyGoaled(EnemyGoaledEvent ev)
        {
            _enemyStore.TryModify(ev.EnemyId, enemy =>
            {
                enemy.IsAlive = false;
                return enemy;
            },
            out var enemy);

            foreach (var goal in _goalStore.Iterate())
            {
                _goalStore.TryModify(goal.Id, goal =>
                {
                    goal.Data.Health -= enemy.Data.Attack;
                    return goal;
                },
                out _);
            }
        }

        private void OnGameStarted(GameStartedEvent ev)
        {
            UnityEngine.Debug.Log($"Game Started");
            Finished = false;
            _currentStage = ev.Stage;
            _waveStartingPub.Publish(new WaveStartingEvent());
        }

        private void OnGameFailed(GameFailedEvent ev)
        {
            UnityEngine.Debug.Log("Game Failed");
            Finished = true;
        }

        private void OnGameClearing(GameClearingEvent ev)
        {
            _gameClearedPub.Publish(new GameClearedEvent());
        }

        private void OnGameCleared(GameClearedEvent ev)
        {
            UnityEngine.Debug.Log("Game Cleared");
            Finished = true;
        }

        private void OnWaveStarted(WaveStartedEvent ev)
        {
            UnityEngine.Debug.Log($"Wave {ev.Wave.Wave} Started");
            _currentWave = ev.Wave;
        }

        private void OnWaveFailed(WaveFailedEvent ev)
        {
            UnityEngine.Debug.Log($"Wave {_currentWave.Wave} Failed");
            _gameFailedPub.Publish(new GameFailedEvent());
        }

        private void OnWaveCleared(WaveClearedEvent ev)
        {
            UnityEngine.Debug.Log($"Wave {_currentWave.Wave} Cleared");
            _waveStartingPub?.Publish(new WaveStartingEvent());
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposables.Build().Dispose();
                _disposed = true;
            }
        }
    }
}
