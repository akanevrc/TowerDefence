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

        private void Start()
        {
            _pedestalStore.Add(new PedestalFactory.FactoryParams(0, PedestalSetting.KindType.Normal, new Vector2(1, 2)));
            _pedestalStore.Add(new PedestalFactory.FactoryParams(1, PedestalSetting.KindType.Normal, new Vector2(2, 3)));
            _unitPlacingPub.Publish(new UnitPlacingEvent(UnitSetting.KindType.Normal.KindToInt(), 0));
        }
    }
}
