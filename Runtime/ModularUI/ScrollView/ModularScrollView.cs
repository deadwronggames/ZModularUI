using DeadWrongGames.ZCommon;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public class ModularScrollView : BaseModularUIComponent<ModularScrollViewConfigSO>
    {
        private const float CHANGE_HIGHLIGHT_TIME_SECONDS = 0.3f;
        
        [SerializeField] Tier _componentTier;

        [Header("Setup")] 
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _borderImage;
        [SerializeField] RectTransform _viewPortRectTransform;
        [SerializeField] VerticalLayoutGroup _contentLayoutGroup;
        [SerializeField] LayoutElement _placeholderTop;
        [SerializeField] LayoutElement _placeholderBottom;
        [SerializeField] Image _scrollbarBackgroundImage;
        [SerializeField] Image _handleImage;
        [SerializeField] Image _scrollbarBorderImage;

        public float ScrollbarValue
        {
            get => _scrollbar.value;
            set => _scrollbar.value = value;
        }
        
        private int _tweenID => GetInstanceID();
        
        private ModularScrollViewProperties _properties;
        RectTransform _contentRectTransform;
        private Scrollbar _scrollbar;
        
        protected override void Setup()
        {
            _contentRectTransform = _contentLayoutGroup.GetComponent<RectTransform>();
            _scrollbar = GetComponentInChildren<Scrollbar>(includeInactive: true);
        }

        protected override void Apply()
        {
            _properties = _theme.GetScrollViewProperties(_componentTier);
            _properties.ApplyTo(
                _backgroundImage, 
                _borderImage,
                _viewPortRectTransform,
                _contentRectTransform,
                _contentLayoutGroup, 
                _placeholderTop,
                _placeholderBottom,
                _scrollbar, 
                _scrollbarBackgroundImage, 
                _handleImage, 
                _scrollbarBorderImage
            );
        }

        private void OnEnable()
        {
            _scrollbar.value = 1f;
            bool isScrollbarNeeded = IsScrollbarNeeded(_contentRectTransform, _viewPortRectTransform);
            _properties.AdjustViewPortPadding(_viewPortRectTransform, isScrollbarNeeded);
        }
        
        private void OnDestroy()
        {
            DOTween.Kill(_tweenID); // Avoid warnings
        }

        public static bool IsScrollbarNeeded(RectTransform contentRectTransform, RectTransform viewPortRectTransform)
        {
            return (contentRectTransform.rect.height > viewPortRectTransform.rect.height);
        }
        
        public void OnValueChanged()
        {
            DOTween.Kill(_tweenID);
            DOTween.Sequence()
                .SetId(_tweenID)
                .Append(GetHandleColorTween(_properties.HandleColorHighlighted, GetTweenRemainingDurationHighlight()))
                .AppendInterval(CHANGE_HIGHLIGHT_TIME_SECONDS)
                .Append(GetHandleColorTween(_properties.HandleColorDefault, _scrollbar.colors.fadeDuration));
            
            return;
            Tween GetHandleColorTween(Color endColor, float duration)
            {
                return DOTween.To(
                    getter: () => _scrollbar.colors.normalColor,
                    setter: c => _scrollbar.SetHandleColorBlock(c, _properties.HandleColorHighlighted),
                    endColor,
                    duration
                );
            }

            float GetTweenRemainingDurationHighlight()
            {
                // Measure where we are between the two colors (0–1)
                float totalDistance = Vector4.Distance((Color)_properties.HandleColorDefault, (Color)_properties.HandleColorHighlighted);
                float remainingDistance = Vector4.Distance(_scrollbar.colors.normalColor, (Color)_properties.HandleColorHighlighted);
                float t = (totalDistance > 0) ? remainingDistance / totalDistance : 0f;
            
                return _scrollbar.colors.fadeDuration * t;
            }
        }
    }
}
