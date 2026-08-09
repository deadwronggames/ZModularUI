using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularScrollViewProperties : BaseModularUIProperty
    {
        // Visual and layout configuration for the scroll view
        [SerializeField] ModularImageProperties _backgroundProperties;
        [SerializeField] UIBorderProperties _borderProperties;
        [SerializeField] [Tooltip("Distance to view border")] int _paddingTop; // CAREFUL! does not work when items are added at runtime (or need to make sure that sibling index is set to last again)
        [SerializeField] [Tooltip("Distance to view border")] int _paddingBottom;
        [SerializeField] [Tooltip("Distance to view border")] int _paddingSides;
        [SerializeField] int _contentSpacing;
        [SerializeField] int _widthScrollbar;
        [SerializeField] ModularImageProperties _scrollbarBackgroundProperties;
        [Tooltip("Usually Color should be set to white. The color in different states gets handled via color fields below")] 
        [SerializeField] ModularImageProperties _handleProperties;
        [SerializeField] UIBorderProperties _scrollbarBorderProperties;
        [SerializeField] RectOffset _handlePadding;
        [SerializeField] ModularColorSO _handleColorDefault;
        [SerializeField] ModularColorSO _handleColorHighlighted;
        
        public ModularColorSO HandleColorDefault => _handleColorDefault;
        public ModularColorSO HandleColorHighlighted => _handleColorHighlighted;

        
        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;
        
        /// <summary>
        /// Applies all visual and layout settings to a scroll view and its parts.
        /// </summary>
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
            // Apply background and border visuals
            _backgroundProperties.ApplyTo(backgroundImage, tweenTime, ease);
            _borderProperties.ApplyTo(borderImage, backgroundImage.rectTransform, tweenTime, ease);

            // Recalculate viewport layout and padding based on scrollbar necessity
            // Scrollbar component should be set to auto-hide and therefore handles itself
            bool isScrollbarNeeded = ModularScrollView.IsScrollbarVisible(contentRectTransform, viewPortRectTransform);
            AdjustViewPortPadding(viewPortRectTransform, isScrollbarNeeded);
            
            // Update content spacing and placeholder offsets
            contentLayoutGroup.spacing = _contentSpacing;
            SetPlaceholderHeight(placeholderTop, desiredPadding: _paddingTop);
            SetPlaceholderHeight(placeholderBottom, desiredPadding: _paddingBottom);
            
            // Apply scrollbar visuals and colors
            scrollbar.SetHandleColorBlock(_handleColorDefault, _handleColorHighlighted);
            _scrollbarBackgroundProperties.ApplyTo(scrollbarBackgroundImage, tweenTime, ease);
            _handleProperties.ApplyTo(handleImage, tweenTime, ease);
            handleImage.rectTransform.SetPadding(_handlePadding, doOverrideSafety: true); // Override safety quick fix: the handle rect transform always gets set into a special state by some scroll component but padding is still no problem
            _scrollbarBorderProperties.ApplyTo(scrollbarBorderImage, scrollbarBackgroundImage.rectTransform, tweenTime, ease);
            AdjustScrollbarRectTransform(scrollbar);
        }
        
        /// <summary>
        /// Adjusts the viewport padding dynamically depending on whether the scrollbar is visible.
        /// </summary>
        public void AdjustViewPortPadding(RectTransform viewPortRectTransform, bool isScrollbarVisible)
        {
            // Right padding becomes larger when scrollbar is present
            int viewPortPaddingRight = (isScrollbarVisible) ? 
                -(_paddingSides + _widthScrollbar + _paddingSides) :
                -(_paddingSides);
            
            // viewPortRectTransform.DoSafeUiModification(() =>
            {
                // offsetMin = left + bottom, offsetMax = right + top (negative = inward)
                viewPortRectTransform.offsetMin = new Vector2(_paddingSides, 0);
                viewPortRectTransform.offsetMax = new Vector2(viewPortPaddingRight, 0);
            }//);
        }

        private void SetPlaceholderHeight(LayoutElement placeholder, int desiredPadding)
        {
            // placeholder height = desired padding minus the already applied view-border padding minus the spacing in the layout group
            int placeholderMinHeight = desiredPadding  - _contentSpacing;
            
            placeholder.gameObject.SetActive(placeholderMinHeight > 0);
            placeholder.minHeight = placeholderMinHeight;
        }

        /// <summary>
        /// Positions the scrollbar along the right edge and applies top/bottom padding.
        /// </summary>
        // ReSharper disable once SuggestBaseTypeForParameter
        private void AdjustScrollbarRectTransform(Scrollbar scrollbar)
        {
            RectTransform rectTransform = scrollbar.GetComponent<RectTransform>();
            
            //rectTransform.DoSafeUiModification(() =>
            {
                rectTransform.offsetMin = new Vector2(-(_paddingSides + _widthScrollbar), _paddingBottom);
                rectTransform.offsetMax = new Vector2(-_paddingSides, -_paddingTop);
            }//);
        }
    }
}