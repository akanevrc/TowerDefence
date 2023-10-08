using System;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [Handler]
    public class UpdateHandler : IDisposable
    {
        [Inject] private MainConfig _mainConfig;
        [Inject] private ISubscriber<UpdateEvent> _updateSub;
        [Inject] private IPublisher<ModelLoopEvent> _modelLoopPub;

        private float _remainingTime = 0.0F;
        private readonly DisposableBagBuilder _disposables = DisposableBag.CreateBuilder();
        private bool _disposed = false;

        public void Init()
        {
            _updateSub.Subscribe(OnUpdate).AddTo(_disposables);
        }

        private void OnUpdate(UpdateEvent ev)
        {
            var modelLoopPeriod = _mainConfig.ModelLoopFrequency;
            var loopCount = (int)((_remainingTime + ev.DeltaTime) / modelLoopPeriod);
            _remainingTime = (_remainingTime + ev.DeltaTime) % modelLoopPeriod;

            for (var i = 0; i < loopCount; i++)
            {
                _modelLoopPub.Publish(new ModelLoopEvent(modelLoopPeriod));
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
