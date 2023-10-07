using System;
using MessagePipe;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class UpdateHandler : IDisposable
    {
        private MainConfig _mainConfig;
        private ISubscriber<UpdateEvent> _updateSub;
        private IPublisher<ModelLoopEvent> _modelLoopPub;

        private float _remainingSecond = 0.0F;
        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public UpdateHandler(MainConfig mainConfig, ISubscriber<UpdateEvent> updateSub, IPublisher<ModelLoopEvent> modelLoopPub)
        {
            _mainConfig = mainConfig;
            _updateSub = updateSub;
            _modelLoopPub = modelLoopPub;

            _updateSub?.Subscribe(OnUpdate).AddTo(_disposables);
        }

        private void OnUpdate(UpdateEvent ev)
        {
            var modelLoopPeriod = _mainConfig.ModelLoopFrequency;
            var loopCount = (int)((_remainingSecond + ev.DeltaSecond) / modelLoopPeriod);
            _remainingSecond = (_remainingSecond + ev.DeltaSecond) % modelLoopPeriod;

            for (var i = 0; i < loopCount; i++)
            {
                _modelLoopPub?.Publish(new ModelLoopEvent(modelLoopPeriod));
            }
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
