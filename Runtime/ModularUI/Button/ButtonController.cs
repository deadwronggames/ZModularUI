using DeadWrongGames.ZServices.EventChannels;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.EventSystems;
    
namespace DeadWrongGames.ZModularUI
{
    public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] Broadcaster[] _onClickResponses;
        [SerializeField] Broadcaster[] _onDeselectResponses;
        [SerializeField] bool _isInteractable = true;
        [SerializeField] bool _isSelectable;

        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }

        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            foreach (Broadcaster response in _onClickResponses)
                response.Broadcast();  
        }
    }
}