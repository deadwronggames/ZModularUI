using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularDropdownProperties : BaseModularUIProperty
    {
        [Header("Button")]
        [SerializeField] ModularImageProperties _buttonBackgroundProperties;
        [SerializeField] CommonTextProperties _buttonTextProperties;
        // TODO [SerializeField] ModularImageProperties _buttonArrowProperties;
        // TODO [SerializeField] int _buttonArrowWidth;
        [SerializeField] int _buttonLabelPaddingLeft, _buttonLabelPaddingRight;
        [SerializeField] UIBorderProperties _buttonBorderProperties;

        [Header("Menu")] 
        [SerializeField] int _menuHeight;
        [SerializeField] ModularImageProperties _menuBackgroundProperties;
        [SerializeField] int _itemHeight;
        [Tooltip("Top, bottom and right distance between scrollbar and menu border ")] 
        [SerializeField] int _scrollbarPadding;
        [SerializeField] int _scrollbarWidth;
        [SerializeField] int _trackWidth;
        [SerializeField] ModularImageProperties _trackProperties;
        [SerializeField] ModularImageProperties _handleProperties;
        [SerializeField] UIBorderProperties _menuBorderProperties;
        
        [Header("Colors")]
        [SerializeField] ModularColorSO _contentColorDefault;
        [SerializeField] ModularColorSO _contentColorHighlighted;
        [SerializeField] ModularColorSO _handleColorDefault;
        [SerializeField] ModularColorSO _handleColorHighlighted;
        
        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {

        }
    }
}