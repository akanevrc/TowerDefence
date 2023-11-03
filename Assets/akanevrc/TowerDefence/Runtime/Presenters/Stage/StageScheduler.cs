using System.Linq;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class StageScheduler
    {
        [Inject] private readonly SettingStore<StageNumber, StageSetting> _stageSettingStore;
        [Inject] private readonly SettingStore<WaveNumber, EnemyWaveSetting> _enemyWaveSettingStore;
        [Inject] private readonly IPublisher<EnemyScheduledEvent> _enemyScheduledPub;
        [Inject] private readonly IPublisher<GameStartedEvent> _gameStartedPub;
        [Inject] private readonly IPublisher<GameClearingEvent> _gameClearingPub;
        [Inject] private readonly IPublisher<WaveStartedEvent> _waveStartedPub;
        [Inject] private readonly ISubscriber<ModelLoopEvent> _modelLoopSub;
        [Inject] private readonly ISubscriber<GameStartingEvent> _gameStartingSub;
        [Inject] private readonly ISubscriber<GameFailedEvent> _gameFailedSub;
        [Inject] private readonly ISubscriber<GameClearedEvent> _gameClearedSub;
        [Inject] private readonly ISubscriber<WaveStartingEvent> _waveStartingSub;
        [Inject] private readonly ISubscriber<WaveFailedEvent> _waveFailedSub;
        [Inject] private readonly ISubscriber<WaveClearedEvent> _waveClearedSub;

        public bool Finished { get; private set; } = true;

        private StageSchedule _stageSchedule;
        private EnemySchedule _currentEnemySchedule;
        private int _currentScheduleIndex = -1;
        private int _currentEnemyIndex = 0;
        private float _totalTime = 0.0F;
        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public void Init()
        {
            _modelLoopSub.Subscribe(OnModelLoop).AddTo(_disposables);
            _gameStartingSub.Subscribe(OnGameStarting).AddTo(_disposables);
            _gameFailedSub.Subscribe(OnGameFailed).AddTo(_disposables);
            _gameClearedSub.Subscribe(OnGameCleared).AddTo(_disposables);
            _waveStartingSub.Subscribe(OnWaveStarting).AddTo(_disposables);
            _waveFailedSub.Subscribe(OnWaveFailed).AddTo(_disposables);
            _waveClearedSub.Subscribe(OnWaveCleared).AddTo(_disposables);
        }

        public void SetStage(StageNumber stage)
        {
            if (_stageSettingStore.Settings.TryGetValue(stage, out var stageSetting))
            {
                var waves =
                    Enumerable.Range(1, stageSetting.WaveCount)
                    .Select(wave => new WaveNumber() { World = stage.World, Stage = stage.Stage, Wave = wave });
                var enemySchedules =
                    waves
                    .Select(wave => (tryGetValue: _enemyWaveSettingStore.Settings.TryGetValue(wave, out var setting), setting))
                    .Where(z => z.tryGetValue)
                    .Select(z => new EnemySchedule(z.setting.Kind, z.setting.EnemySeries));
                _stageSchedule = new StageSchedule(stage, enemySchedules);

                Finished = false;
            }
            else
            {
                _stageSchedule = new StageSchedule(stage, Enumerable.Empty<EnemySchedule>());
            }
        }

        private void OnModelLoop(ModelLoopEvent ev)
        {
            _totalTime += ev.DeltaTime;

            while
            (
                !Finished &&
                _currentEnemyIndex < _currentEnemySchedule.ReservedEnemies.Length &&
                _currentEnemySchedule.ReservedEnemies[_currentEnemyIndex].SpawnTime <= _totalTime
            )
            {
                _enemyScheduledPub.Publish(new EnemyScheduledEvent(_currentEnemySchedule.ReservedEnemies[_currentEnemyIndex]));
                _currentEnemyIndex++;
                if (_currentEnemyIndex == _currentEnemySchedule.ReservedEnemies.Length)
                {
                    Finished = true;
                }
            }
        }

        private void OnGameStarting(GameStartingEvent ev)
        {
            if (_stageSchedule.EnemySchedules.Any())
            {
                Finished = true;
                _currentScheduleIndex = -1;
                _currentEnemyIndex = 0;
                _totalTime = 0.0F;

                _gameStartedPub.Publish(new GameStartedEvent(_stageSchedule.Stage));
            }
            else
            {
                _gameClearingPub.Publish(new GameClearingEvent());
            }
        }

        private void OnGameFailed(GameFailedEvent ev)
        {
            Finished = true;
        }

        private void OnGameCleared(GameClearedEvent ev)
        {
            Finished = true;
        }

        private void OnWaveStarting(WaveStartingEvent ev)
        {
            _currentScheduleIndex++;
            if (_currentScheduleIndex < _stageSchedule.EnemySchedules.Count)
            {
                Finished = false;
                _currentEnemyIndex = 0;
                _totalTime = 0.0F;
                _currentEnemySchedule = _stageSchedule.EnemySchedules.Values.ElementAt(_currentScheduleIndex);
                
                _waveStartedPub.Publish(new WaveStartedEvent(_currentEnemySchedule.Wave));
            }
            else
            {
                _gameClearingPub.Publish(new GameClearingEvent());
            }
        }

        private void OnWaveFailed(WaveFailedEvent ev)
        {
            Finished = true;
        }

        private void OnWaveCleared(WaveClearedEvent ev)
        {
            Finished = true;
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
