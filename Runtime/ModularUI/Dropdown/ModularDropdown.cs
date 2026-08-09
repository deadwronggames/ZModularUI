using System;
using System.Collections.Generic;
using DeadWrongGames.ZCommon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    // TODO
    // just do dropdowns from scratch
    // maybe have scroll view get some of the properties from dropdown button
    // adjust scroll view height to show X items
    public class ModularDropdown : BaseModularUIComponent<ModularDropdownConfigSO>
    {
        [Serializable]
        public struct DropdownButtonComponents
        {
            public RectTransform RectTransform;
            public Image BackgroundImage;
            public TMP_Text ButtonText;
            // TODO public Image ArrowImage; 
            // TODO public LayoutElement ArrowLayoutElement; 
            public Image BorderImage;
        }
        
        [Serializable]
        public struct DropdownMenuComponents
        {
            public RectTransform RectTransform;
            public RectTransform ContentRectTransform;
            public Image BackgroundImage;
            public Toggle ItemToggle;
            public Scrollbar Scrollbar;
            public Image BorderImage;
        }
        
        [SerializeField] Tier _componentTier;
        
        [Header("Setup")]
        [SerializeField] RectTransform _fillAreaRectTransform;
        [SerializeField] Image _fillImage;
        [SerializeField] RectTransform _slideAreaRectTransform;
        [SerializeField] Image _handleImage;

        [Header("Dropdown Options")] 
        [SerializeField] List<TMP_Dropdown.OptionData> _options;

        private TMP_Dropdown _dropdown;
        
        protected override void Setup()
        {
            _dropdown = GetComponentInChildren<TMP_Dropdown>(includeInactive: true);
        }

        protected override void Apply()
        {
            _dropdown.options = _options;
        }
    }
}