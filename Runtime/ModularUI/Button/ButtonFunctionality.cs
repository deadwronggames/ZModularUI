using System;
using DeadWrongGames.ZServices.EventChannel;
using UnityEngine;
using UnityEngine.EventSystems;
    
namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// Defines visual and audio feedback for button interactions.
    /// Instances of this ScriptableObject can be assigned to the feedback fields of the 
    /// <see cref="ButtonFunctionality"/> class to control how buttons respond to various 
    /// user interactions such as hover or press.
    /// </summary>

    [RequireComponent(typeof(ModularButton))]
    public class ButtonFunctionality : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] bool _isInteractable = true;
        [SerializeField] bool _isSelectable;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackHover;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackPress;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackClick; // must only be non-persistent oneshot effects
        [SerializeField] ButtonInteractionFeedbackSO _feedbackSelect;
        
        [Header("Button Responses")]
        [SerializeField] Broadcaster[] _onClickResponses;
        [SerializeField] Broadcaster[] _onDeselectResponses;
        
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }
        public event Action<ButtonFunctionality> OnSelectEvent;
        
        private ModularButton _modularButton;
        
        // TODO could / should refactor into state machine
        private bool _isHovered;
        private bool _isPressed;
        private bool _isSelected;
        private bool _isSelectedFixed; // from outside e.g. when this button is used to select tabs and the tab is selected by other means e.g. via keyboard. TODO don't think I need it anymore but see after I have implemented tabs and keyboard navigation

        private void Awake()
        {
            _modularButton = GetComponent<ModularButton>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isInteractable) return;

            _isHovered = true;
            if (!_isSelected && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            _isHovered = false;
            _isPressed = false;
            if (_isSelected && _feedbackSelect != null) _modularButton.DoFeedback(_feedbackSelect, doOneshots: false);
            else _modularButton.EndFeedback();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isInteractable) return;

            _isPressed = true;
            if (!_isSelectedFixed && _feedbackPress != null) _modularButton.DoFeedback(_feedbackPress, doOneshots: true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isInteractable) return;
            
            _isPressed = false;
            if (_feedbackClick != null) _modularButton.DoFeedback(_feedbackClick, doOneshots: true);
            if (_isSelectable && !_isSelectedFixed)
            {
                if (_isSelected) Deselect();
                else
                {
                    Select();
                    if (_feedbackSelect == null && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
                }
            }
            else if (!_isSelectedFixed && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
            
            foreach (Broadcaster response in _onClickResponses)
                response.Broadcast();  
        }
        
        public void Select(bool doFixedSelect = false)
        {
            if (!_isSelectable) return;

            OnSelectEvent?.Invoke(this);
            _isSelected = true;
            if (doFixedSelect) _isSelectedFixed = true;
            if ((!_isPressed || _isSelectedFixed) && _feedbackSelect != null) _modularButton.DoFeedback(_feedbackSelect, doOneshots: true);
        }
        
        public void Deselect(bool doDeselectFixed = false)
        {
            if (!_isSelectable) return;
            if (_isSelectedFixed && !doDeselectFixed) return;

            _isSelected = false;
            _isSelectedFixed = false;
            if (_isPressed && _feedbackPress != null) _modularButton.DoFeedback(_feedbackPress, doOneshots: false);
            else if (_isHovered && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
            else _modularButton.EndFeedback();
            
            foreach (Broadcaster response in _onDeselectResponses)
                response.Broadcast(); 
        }
    }
}