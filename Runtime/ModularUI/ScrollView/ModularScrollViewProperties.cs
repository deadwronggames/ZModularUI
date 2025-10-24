using DG.Tweening;
using System;
using System.Threading.Tasks;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularScrollViewProperties : BaseModularUIProperty
    {
        [SerializeField] ModularImageProperties _backgroundProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] int _paddingTop;
        [SerializeField] int _paddingBottom;
        [SerializeField] int _paddingSides;
        [SerializeField] int _contentSpacing;
        [SerializeField] int _widthScrollbar;
        [SerializeField] ModularImageProperties _scrollbarBackgroundProperties;
        [Tooltip("Usually Color should be set to white. The color in different states gets handled via color fields below")] 
        [SerializeField] ModularImageProperties _handleProperties;
        [FormerlySerializedAs("_borderProperties")]
        [SerializeField] UIBorderProperties _scrollbarBorderProperties;
        [SerializeField] RectOffset _handlePadding;
        [SerializeField] ModularColorSO _handleColorDefault;
        [SerializeField] ModularColorSO _handleColorHighlighted;
        
        public ModularColorSO HandleColorDefault => _handleColorDefault;
        public ModularColorSO HandleColorHighlighted => _handleColorHighlighted;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        public void ApplyTo(
            Image backgroundImage, 
            Image borderImage, 
            RectTransform viewPortRectTransform,
            RectTransform contentRectTransform,
            VerticalLayoutGroup contentLayoutGroup, 
            LayoutElement placeholderTop, 
            LayoutElement placeholderBottom, 
            Scrollbar scrollbar, 
            Image scrollbarBackgroundImage, 
            Image handleImage, 
            Image scrollbarBorderImage, 
            float tweenTime = 0f, Ease ease = Ease.OutQuad
        )
        {
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);

            bool isScrollbarNeeded = ModularScrollView.IsScrollbarNeeded(contentRectTransform, viewPortRectTransform);
            AdjustViewPortPadding(viewPortRectTransform, isScrollbarNeeded);
            
            contentLayoutGroup.spacing = _contentSpacing;
            placeholderTop.minHeight = _paddingTop - _borderProperties.ContentPadding.top - _contentSpacing;
            placeholderBottom.minHeight = _paddingBottom - _borderProperties.ContentPadding.bottom - _contentSpacing;
            
            scrollbar.SetHandleColorBlock(_handleColorDefault, _handleColorHighlighted);
            _scrollbarBackgroundProperties.ApplyTo(scrollbarBackgroundImage, tweenTime, ease);
            _handleProperties.ApplyTo(handleImage, tweenTime, ease);
            handleImage.rectTransform.SetPadding(_handlePadding, doOverrideSafety: true); // Override safety quick fix: the handle rect transform always gets set into a special state by some scroll component but padding is still no problem
            _scrollbarBorderProperties.ApplyTo(scrollbarBorderImage, scrollbarBackgroundImage.rectTransform, tweenTime, ease);
            AdjustScrollbarPositionAndPadding(scrollbar);
        }
        
        public void AdjustViewPortPadding(RectTransform viewPortRectTransform, bool isScrollbarNeeded)
        {
            if (viewPortRectTransform == null) return; // Ignore obsolete callbacks without error

            float viewPortPaddingRight = (isScrollbarNeeded) ? 
                -(_paddingSides + _widthScrollbar + (_paddingSides - _borderProperties.ContentPadding.right)) : // TODO when and why do I need the minus signs in front?
                -_paddingSides;
            ModularUIHelpers.DoSafeUiModification(() =>
            {
                viewPortRectTransform.offsetMin = new Vector2(_paddingSides, _borderProperties.ContentPadding.bottom); // left and bottom
                viewPortRectTransform.offsetMax = new Vector2(0, -_borderProperties.ContentPadding.top); // right (does not matter) and top TODO why do I need the minus sign?
                viewPortRectTransform.offsetMax = viewPortRectTransform.offsetMax.With(x: viewPortPaddingRight); // right
            });
        }

        // ReSharper disable once SuggestBaseTypeForParameter
        private void AdjustScrollbarPositionAndPadding(Scrollbar scrollbar)
        {
            RectTransform rectTransform = scrollbar.GetComponent<RectTransform>();
            ModularUIHelpers.DoSafeUiModification(() =>
            {
                rectTransform.offsetMax = new Vector2(-_paddingSides, -_paddingTop);
                rectTransform.offsetMin = new Vector2(rectTransform.offsetMax.x - _widthScrollbar, _paddingBottom);
            });
        }
    }
}
