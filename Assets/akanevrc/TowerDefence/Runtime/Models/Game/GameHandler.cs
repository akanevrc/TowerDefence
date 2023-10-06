using System;
using MessagePipe;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class GameHandler : IDisposable
    {
        private ISubscriber<ModelLoopEvent> _modelLoopSub;

        private DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public GameHandler(ISubscriber<ModelLoopEvent> modelLoopSub)
        {
            _modelLoopSub = modelLoopSub;

            _modelLoopSub?.Subscribe(OnModelLoop).AddTo(_disposables);
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
