using DG.Tweening;
using System;
using System.Threading.Tasks;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularScrollViewProperties : BaseModularUIProperty
    {
        [SerializeField] float _contentSpacing;
        [SerializeField] ModularImageProperties _backgroundProperties;
        [Tooltip("Usually Color should be set to white. The color in different states gets handled via color fields below")] 
        [SerializeField] ModularImageProperties _handleProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] RectOffset _handlePadding;
        [SerializeField] ModularColorSO _handleColorDefault;
        [SerializeField] ModularColorSO _handleColorHighlighted;

        public float ContentSpacing => _contentSpacing;
        public ModularImageProperties BackgroundProperties => _backgroundProperties;
        public ModularImageProperties HandleProperties => _handleProperties;
        public UIBorderProperties BorderProperties => _borderProperties;
        public RectOffset HandlePadding => _handlePadding;
        public ModularColorSO HandleColorDefault => _handleColorDefault;
        public ModularColorSO HandleColorHighlighted => _handleColorHighlighted;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(VerticalLayoutGroup contentLayoutGroup, Scrollbar scrollbar, Image backgroundImage, Image handleImage, Image borderImage, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            contentLayoutGroup.spacing = _contentSpacing;

            scrollbar.SetHandleColorBlock(_handleColorDefault, _handleColorHighlighted);
            
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _handleProperties.ApplyTo(handleImage, tweenTime, ease);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);
            
            handleImage.rectTransform.SetPadding(_handlePadding, doOverrideSafety: true); // Override safety quick fix: the handle rect transform always gets set into a special state by some scroll component but padding is still no problem
        }
    }
}
