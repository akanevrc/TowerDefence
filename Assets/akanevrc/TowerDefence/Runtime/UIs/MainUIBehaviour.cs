using UnityEngine;
using UnityEngine.UIElements;
using MessagePipe;
using VContainer;

namespace akanevrc.TowerDefence
{
    [MonoBehaviour]
    public class MainUIBehaviour : MonoBehaviour
    {
        [Inject] private readonly EntityStore<Unit, UnitFactory.FactoryParams> _unitStore;
        [Inject] private readonly UnitLevelUpdater _unitLevelUpdater;
        [Inject] private readonly IPublisher<TappedAtEvent<Unit>> _tappedAtUnitPub;
        [Inject] private readonly IPublisher<TappedAtEvent<Pedestal>> _tappedAtPedestalPub;
        [Inject] private readonly IPublisher<UnitPlacingEvent> _unitPlacingPub;

        private VisualElement _root;
        private ScrollView _scrollView;
        private VisualElement _uiPane;
        private Label _entityNameLabel;
        private Button _levelUpButton;

        private int _selectedUnitId = Entity<Unit>.None.Id;

        private void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _scrollView = _root.Q<ScrollView>("scroll-view");
            _uiPane = _root.Q<VisualElement>("ui-pane");
            _entityNameLabel = _root.Q<Label>("entity-name-label");
            _levelUpButton = _root.Q<Button>("level-up-button");

            _root.RegisterCallback<MouseUpEvent>(OnRootMouseUp);
            _uiPane.RegisterCallback<MouseUpEvent>(OnUIPaneMouseUp);
            _levelUpButton.clicked += OnLevelUpButtonClicked;
        }

        private void OnDestroy()
        {
            _root.UnregisterCallback<MouseUpEvent>(OnRootMouseUp);
            _uiPane.UnregisterCallback<MouseUpEvent>(OnUIPaneMouseUp);
            _levelUpButton.clicked -= OnLevelUpButtonClicked;
        }

        private void OnRootMouseUp(MouseUpEvent ev)
        {
            var position = Camera.main.ScreenToWorldPoint(MouseToScreen(ev.mousePosition));

            var unitId = HitAt<Unit, UnitSetting.KindType, UnitBehaviour>(position, "Unit");
            _selectedUnitId = unitId;

            if (unitId != Entity<Unit>.None.Id)
            {
                _entityNameLabel.text = unitId.ToString();

                _tappedAtUnitPub.Publish(new TappedAtEvent<Unit>(unitId));
            }
            else
            {
                _entityNameLabel.text = "Not Selected";

                var pedestalId = HitAt<Pedestal, PedestalSetting.KindType, PedestalBehaviour>(position, "Pedestal");

                if (pedestalId != Entity<Pedestal>.None.Id)
                {
                    _tappedAtPedestalPub.Publish(new TappedAtEvent<Pedestal>(pedestalId));
                    _unitPlacingPub.Publish(new UnitPlacingEvent(UnitSetting.KindType.Normal.KindToInt(), pedestalId));
                }
            }
        }

        private void OnUIPaneMouseUp(MouseUpEvent ev)
        {
            ev.StopPropagation();
        }

        private void OnLevelUpButtonClicked()
        {
            _unitStore.TryModify(_selectedUnitId, unit =>
            {
                _unitLevelUpdater.IncrementLevel(ref unit);
                return unit;
            },
            out _);
        }

        private Vector2 MouseToScreen(Vector2 mousePosition)
        {
            return new Vector2(mousePosition.x, Camera.main.pixelHeight - mousePosition.y);
        }

        private int HitAt<T, TKind, TBehaviour>(Vector2 position, string layerName)
            where T : struct
            where TKind : struct
            where TBehaviour : EntityBehaviour<T, TKind>
        {
            var hit = Physics2D.Raycast(position, Vector2.zero, 0.0F, 1 << LayerMask.NameToLayer(layerName));
            if (hit.collider != null)
            {
                return hit.collider.gameObject.GetComponent<TBehaviour>()?.Id ?? Entity<T>.None.Id;
            }
            else
            {
                return Entity<T>.None.Id;
            }
        }
    }
}
