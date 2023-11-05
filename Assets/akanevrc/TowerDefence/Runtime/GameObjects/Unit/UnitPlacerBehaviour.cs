using UnityEngine;
using Cysharp.Threading.Tasks;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class UnitPlacerBehaviour : MonoBehaviour
    {
        [Inject] private readonly EntityStore<Pedestal, PedestalFactory.FactoryParams> _pedestalStore;
        [Inject] private readonly IPublisher<UnitPlacingEvent> _unitPlacingPub;

        private async void Start()
        {
            await UniTask.Delay(1500);
            
            if (_pedestalStore.TryGet(PedestalSetting.KindType.Normal.KindToInt(), out var pedestal))
            {
                _unitPlacingPub.Publish(new UnitPlacingEvent(UnitSetting.KindType.Normal.KindToInt(), pedestal.Position, pedestal.Id));
            }
        }
    }
}
