using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public partial class MainEntryPoint : IAsyncStartable, ITickable, IDisposable
    {
        [Inject] private readonly IObjectResolver _resolver;
        [Inject] private readonly IPublisher<UpdateEvent> _updatePub;

        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        private partial void HoldHandlers();

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            HoldHandlers();

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
