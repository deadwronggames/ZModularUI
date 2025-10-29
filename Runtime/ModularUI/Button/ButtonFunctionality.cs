using System;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
    
namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// Triggers click, select and deselect responses. Keeps track of button selection state. 
    /// Also defines visual and audio feedback for button interactions.
    /// ScriptableObject feedback instances can be assigned to the feedback fields of the 
    /// this class to control how the button responds to various  user interactions such as hover or press.
    /// Also supports special button behaviour (radio/tab) via <see cref="OnSelectEvent"/>.
    /// </summary>

    [RequireComponent(typeof(ModularButton))]
    public class ButtonFunctionality : SerializedMonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField] bool _isInteractable = true;
        [SerializeField] bool _isSelectable;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackHover;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackPress;
        [SerializeField] ButtonInteractionFeedbackSO _feedbackClick; // must only be non-persistent oneshot effects
        [SerializeField] ButtonInteractionFeedbackSO _feedbackSelect;
        
        [Header("Button Responses")]
        [SerializeField] IInvokable[] _onClickResponses = {};
        [SerializeField] IInvokable[] _onDeselectResponses = {};
        
        public bool IsInteractable { get => _isInteractable; set => _isInteractable = value; }
        
        /// <summary>
        /// Fired when this button gets selected.
        /// Used by controllers like <see cref="RadioButtonController"/>.
        /// </summary>
        public event Action<ButtonFunctionality> OnSelectEvent;

        private ModularButton _modularButton => ZMethods.LazyInitialization(ref _modularButtonBacking, GetComponent<ModularButton>);
        private ModularButton _modularButtonBacking;
        
        // TODO could / should refactor into state machine
        private bool _isHovered;
        private bool _isPressed;
        private bool _isSelected;
        private bool _isSelectedFixed; // from outside e.g. when this button is used to select tabs and the tab is selected by other means e.g. via keyboard.
        
        
        #region Pointer interaction logic
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
            
            // Handle select/deselect toggle if this is a selectable button
            if (_isSelectable && !_isSelectedFixed)
            {
                if (_isSelected) Deselect();
                else
                {
                    Select();
                    
                    // If no explicit select feedback exists, use hover fallback
                    if (_feedbackSelect == null && _feedbackHover != null) 
                        _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
                }
            }
            else if (!_isSelectedFixed && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
            
            // Trigger any assigned click responses
            // if (_onDeselectResponses != null)
            {
                foreach (IInvokable response in _onClickResponses) response.Invoke();
            }
        }
        #endregion
        
        
        #region Select and Deselect
        public void Select(bool doFixedSelect = false)
        {
            if (!_isSelectable) return;

            if (doFixedSelect) _isSelectedFixed = true;
            if (_isSelected) return;
            
            _isSelected = true;
            OnSelectEvent?.Invoke(this); // Notify controllers (e.g. <see cref="RadioButtonController"/>) of selection
            if ((!_isPressed || _isSelectedFixed) && _feedbackSelect != null) _modularButton.DoFeedback(_feedbackSelect, doOneshots: true);
        }
        
        public void Deselect(bool doDeselectFixed = false)
        {
            if (!_isSelectable) return;
            if (_isSelectedFixed && !doDeselectFixed) return;
            if (!_isSelected) return;

            _isSelected = false;
            _isSelectedFixed = false;
            
            // Restore appropriate feedback depending on current interaction state
            if (_isPressed && _feedbackPress != null) _modularButton.DoFeedback(_feedbackPress, doOneshots: false);
            else if (_isHovered && _feedbackHover != null) _modularButton.DoFeedback(_feedbackHover, doOneshots: false);
            else _modularButton.EndFeedback();
            
            // Trigger any assigned deselect responses
            // if (_onDeselectResponses != null)
            {
                foreach (IInvokable response in _onDeselectResponses) response.Invoke();
            }
        }
        #endregion
    }
}