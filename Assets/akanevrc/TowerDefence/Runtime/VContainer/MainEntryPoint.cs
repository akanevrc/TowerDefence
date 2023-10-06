using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public class MainEntryPoint : IAsyncStartable, ITickable, IDisposable
    {
        private readonly IObjectResolver _resolver;
        private readonly UpdateHandler _updateHandler;
        private readonly GameHandler _gameHandler;
        private readonly IPublisher<UpdateEvent> _updatePub;

        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public MainEntryPoint
        (
            IObjectResolver resolver,
            UpdateHandler updateHandler,
            GameHandler gameHandler,
            IPublisher<UpdateEvent> updatePub
        )
        {
            _resolver = resolver;
            _updateHandler = updateHandler;
            _gameHandler = gameHandler;
            _updatePub = updatePub;

            _disposables.Add(_updateHandler);
            _disposables.Add(_gameHandler);
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield();
        }

        public void Tick()
        {
            _updatePub?.Publish(new UpdateEvent(Time.deltaTime));
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
