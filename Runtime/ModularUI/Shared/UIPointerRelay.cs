using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DeadWrongGames.ZModularUI
{
    public class UIPointerRelay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] UnityEvent<PointerEventData> _onPointerEnter;
        [SerializeField] UnityEvent<PointerEventData> _onPointerExit;
        [SerializeField] UnityEvent<PointerEventData> _onPointerDown;
        [SerializeField] UnityEvent<PointerEventData> _onPointerClick;

        public void OnPointerEnter(PointerEventData eventData) => _onPointerEnter?.Invoke(eventData);
        public void OnPointerExit(PointerEventData eventData) => _onPointerExit?.Invoke(eventData);
        public void OnPointerDown(PointerEventData eventData) => _onPointerDown?.Invoke(eventData);
        public void OnPointerClick(PointerEventData eventData) => _onPointerClick?.Invoke(eventData);
    }
}
