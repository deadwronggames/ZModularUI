using System;
using DeadWrongGames.ZUtils;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public class TabButtonController : MonoBehaviour
    {
        [Tooltip("Each tab button should have a corresponding tab view GameObject in the same order.")]
        [SerializeField] GameObject[] _tabViewGOs;
        
        private RadioButtonController _radioButtonController;
        private ButtonFunctionality[] _tabButtons => _radioButtonController.RadioButtons; // For convenience

        private void Awake()
        {
            // Find the radio button controller and initialize it
            _radioButtonController = GetComponentInChildren<RadioButtonController>(includeInactive: true);
            _radioButtonController.Configure();
            
            // Sanity check: ensure buttons and views align
            if (_tabButtons.Length != _tabViewGOs.Length) $"On {name}: Number of tab buttons does not match number of tab views.".Log(level: ZMethodsDebug.LogLevel.Error);
        }

        private void OnEnable()
        {
            // Subscribe to tab selection events
            foreach (ButtonFunctionality tabButton in _tabButtons)
                tabButton.OnSelectEvent += ActivateTabView;

            // Activate initial tab if defined, otherwise disable all
            if (_radioButtonController.InitiallySelectedButtonIndex.IndexIsInRange(_tabButtons.Length))
                ActivateTabView(_tabButtons[_radioButtonController.InitiallySelectedButtonIndex]);
            else DeactivateAllTabViews();
        }
        
        private void OnDisable()
        {
            // Unsubscribe to tab selection events
            foreach (ButtonFunctionality tabButton in _tabButtons)
                tabButton.OnSelectEvent -= ActivateTabView;
        }

        private void DeactivateAllTabViews()
        {
            foreach (GameObject go in _tabViewGOs)
                go.SetActive(false);
        }
        
        private void ActivateTabView(ButtonFunctionality tabButton)
        {
            // Enable only the tab view corresponding to the selected button
            int tabIndex = Array.IndexOf(_tabButtons, tabButton);
            for (int i = 0; i < _tabViewGOs.Length; i++)
                _tabViewGOs[i].SetActive(i == tabIndex);
        }
    }
}
