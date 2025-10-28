using System;
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
        // All buttons that are children to this
        private ButtonFunctionality[] _radioButtons = Array.Empty<ButtonFunctionality>();

        private void OnEnable()
        {
            FindButtons();
            SubscribeToButtons();
        }

        private void OnDisable()
        {
            UnsubscribeFromButtons();
        }
        
        private void FindButtons()
        {
            _radioButtons = transform.GetComponentsInChildren<ButtonFunctionality>(includeInactive: true);
        }

        private void SubscribeToButtons()
        {
            foreach (ButtonFunctionality radioButton in _radioButtons) 
                radioButton.OnSelectEvent += OnRadioButtonSelect;
        }
        
        private void UnsubscribeFromButtons()
        {
            foreach (ButtonFunctionality radioButton in _radioButtons) 
                radioButton.OnSelectEvent -= OnRadioButtonSelect;
        }

        // Ensure only the clicked button stays selected
        private void OnRadioButtonSelect(ButtonFunctionality selectedButton)
        {
            foreach (ButtonFunctionality radioButton in _radioButtons)
                if (radioButton != selectedButton) radioButton.Deselect();
        }
    }
}
