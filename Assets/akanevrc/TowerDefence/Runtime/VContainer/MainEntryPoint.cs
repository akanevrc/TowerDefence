using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace akanevrc.TowerDefence
{
    public partial class MainEntryPoint : IAsyncStartable, ITickable
    {
        [Inject] private readonly IPublisher<UpdateEvent> _updatePub;

        partial void Init();

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            Init();

            await UniTask.Yield();
        }

        public void Tick()
        {
            _updatePub?.Publish(new UpdateEvent(Time.deltaTime));
        }
    }
}
