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
        [SerializeField] VerticalLayoutGroup _contentLayoutGroup;
        [SerializeField] Image _backgroundImage;
        [SerializeField] Image _handleImage;
        [SerializeField] Image _borderImage;

        private int _tweenID => GetInstanceID();
        
        private Color _handleColorDefault;
        private Color _handleColorHighlighted;
        
        public float ScrollbarValue
        {
            get => _scrollbar.value;
            set => _scrollbar.value = value;
        }

        private Scrollbar _scrollbar;
        protected override void Setup()
        {
            _scrollbar = GetComponentInChildren<Scrollbar>(includeInactive: true);
        }

        protected override void Apply()
        {
            ModularScrollViewProperties properties = _theme.GetScrollViewProperties(_componentTier) ;
            properties.ApplyTo(_contentLayoutGroup, _scrollbar, _backgroundImage, _handleImage, _borderImage);
            _handleColorDefault = properties.HandleColorDefault;
            _handleColorHighlighted = properties.HandleColorHighlighted;
        }
        
        private void OnDestroy()
        {
            DOTween.Kill(_tweenID); // Avoid warnings
        }

        public void OnValueChanged()
        {
            DOTween.Kill(_tweenID);
            DOTween.Sequence()
                .SetId(_tweenID)
                .Append(GetHandleColorTween(_handleColorHighlighted, GetTweenRemainingDurationHighlight()))
                .AppendInterval(CHANGE_HIGHLIGHT_TIME_SECONDS)
                .Append(GetHandleColorTween(_handleColorDefault, _scrollbar.colors.fadeDuration));
            
            return;
            Tween GetHandleColorTween(Color endColor, float duration)
            {
                return DOTween.To(
                    () => _scrollbar.colors.normalColor,
                    c => _scrollbar.SetHandleColorBlock(c, _handleColorHighlighted),
                    endColor,
                    duration
                );
            }

            float GetTweenRemainingDurationHighlight()
            {
                // Measure where we are between the two colors (0–1)
                float totalDistance = Vector4.Distance(_handleColorDefault, _handleColorHighlighted);
                float remainingDistance = Vector4.Distance(_scrollbar.colors.normalColor, _handleColorHighlighted);
                float t = (totalDistance > 0) ? remainingDistance / totalDistance : 0f;
            
                return _scrollbar.colors.fadeDuration * t;
            }
        }

        
    }
}
