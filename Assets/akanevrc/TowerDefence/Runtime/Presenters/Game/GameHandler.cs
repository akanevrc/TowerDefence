using System;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class GameHandler : IDisposable
    {
        [Inject] private ISubscriber<ModelLoopEvent> _modelLoopSub;

        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public void Init()
        {
            _modelLoopSub.Subscribe(OnModelLoop).AddTo(_disposables);
        }

        private void OnModelLoop(ModelLoopEvent ev)
        {
            UnityEngine.Debug.Log("GameHandler.OnModelLoop");
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
