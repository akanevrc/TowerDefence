using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public class MainEntryPoint : IAsyncStartable, ITickable, IDisposable
    {
        private readonly IObjectResolver _resolver;

        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public MainEntryPoint
        (
            IObjectResolver resolver
        )
        {
            _resolver = resolver;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield();
        }

        public void Tick()
        {
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
