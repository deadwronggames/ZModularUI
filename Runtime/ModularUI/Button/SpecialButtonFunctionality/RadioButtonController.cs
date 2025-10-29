using System;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// Controls a group of <see cref="ButtonFunctionality"/> components to behave as radio buttons.
    /// Ensures only one button in the same parent group stays selected at a time.
    /// Attach this component so that all radio buttons are children to this.
    /// It automatically finds all sibling buttons and manages their selection state.
    /// </summary>
    public class RadioButtonController : MonoBehaviour
    {
        [Tooltip("If checked, buttons can only be deselected by selecting another radio button from the group.")]
        [SerializeField] bool _doFixedSelect;
        
        [Tooltip("Zero based index. If out of range, no button will be active initially.")]
        [SerializeField] int _initiallySelectedButtonIndex = -1;

        // All buttons that are children to this
        public ButtonFunctionality[] RadioButtons { get; private set; } = Array.Empty<ButtonFunctionality>();
        public int InitiallySelectedButtonIndex => _initiallySelectedButtonIndex;
        
        
        private int _selectedButtonIndex;

        public void Configure()
        {
            UnsubscribeFromButtons(); // Prevent duplicate subscriptions if reconfigured
            FindButtons(); // Find all radio buttons under this controller
            SubscribeToButtons(); // Listen to selection events
            
            SelectButtonAtIndex(_initiallySelectedButtonIndex); // Set initial tab
        }

        public void ChangeSelectedButtonByOffset(int indexChange)
        {
            int newIndex = _selectedButtonIndex + indexChange;
            SelectButtonAtIndex(newIndex);
        }

        private void OnEnable()
        {
            Configure();
        }

        private void OnDisable()
        {
            UnsubscribeFromButtons();
        }
        
        private void FindButtons()
        {
            RadioButtons = transform.GetComponentsInChildren<ButtonFunctionality>(includeInactive: true);
        }

        private void SubscribeToButtons()
        {
            foreach (ButtonFunctionality radioButton in RadioButtons) 
                radioButton.OnSelectEvent += OnRadioButtonSelect;
        }
        
        private void UnsubscribeFromButtons()
        {
            foreach (ButtonFunctionality radioButton in RadioButtons) 
                radioButton.OnSelectEvent -= OnRadioButtonSelect;
        }

        private void SelectButtonAtIndex(int newSelectedButtonIndex)
        {
            // Ignore invalid index
            if (!newSelectedButtonIndex.IndexIsInRange(RadioButtons.Length)) return;
            
            // Trigger selection on target
            RadioButtons[newSelectedButtonIndex].Select(doFixedSelect: _doFixedSelect);
            _selectedButtonIndex = newSelectedButtonIndex;
        }

        // Ensure only the clicked button stays selected
        private bool _suppressCallback; // Just to be safe, prevent recursive OnSelectEvent calls when updating button states
        private void OnRadioButtonSelect(ButtonFunctionality selectedButton)
        {
            if (_suppressCallback) return; // Avoid re-entrancy if selection triggers nested events
            _suppressCallback = true;
            
            foreach (ButtonFunctionality radioButton in RadioButtons)
            {
                if (radioButton != selectedButton) radioButton.Deselect(doDeselectFixed: _doFixedSelect);
                if (_doFixedSelect && radioButton == selectedButton) radioButton.Select(doFixedSelect: true);
            }
            
            _suppressCallback = false;
        }

        /// <summary>
        /// Utility action that shifts selection by a relative offset.
        /// Can be triggered e.g. as response by ModularUI buttons.
        /// </summary>
        [Serializable]
        private struct ChangeSelectedButtonByOffsetAction : IInvokable
        {
            
            public RadioButtonController RadioButtonController;
            public int IndexOffset;

            public void Invoke() => RadioButtonController.ChangeSelectedButtonByOffset(IndexOffset);
        }
    }
}
