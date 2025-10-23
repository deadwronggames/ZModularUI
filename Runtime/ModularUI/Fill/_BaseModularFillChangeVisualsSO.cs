using System;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public abstract class BaseModularFillChangeEffectSO : ScriptableObject
    {
        private const float CHANGE_TWEEN_DURATION = 0.3f;
        private static readonly AnimationCurve CHANGE_TWEEN_EASE = new( // Kinda like a sigmoid curve but shifted to the left. If you want to see the curve in the inspector, just make this a SerializedField
            new Keyframe(0f, 0f, 0f, 0f),
            new Keyframe(0.1f, 0.3f, 3f, 3f),
            new Keyframe(0.33f, 0.75f, 1f, 1f),
            new Keyframe(1f, 1f, 0f, 0f)
        );
        
        public abstract void Play(Image mainFill, Image changeIndicator, float oldFillValue, float newFillValue, bool doTweening, int tweenId, bool useUnscaledTime, Action onCompleteAction);

        /// <summary>
        /// Default effect for changing fill amount, tweened or instant.
        /// Just Uses the main fill image, no additional change indicator.
        /// </summary>
        public static void PlayChange(Image fillImage, float newFillValue, bool doTweening, int tweenId, bool useUnscaledTime = false, Action onCompleteAction = null)
        {
            if (doTweening)
            {
                fillImage.DOFillAmount(newFillValue, CHANGE_TWEEN_DURATION)
                    .SetId(tweenId)
                    .SetEase(CHANGE_TWEEN_EASE)
                    .SetUpdate(useUnscaledTime)
                    .OnComplete(() => onCompleteAction?.Invoke());
            }
            else fillImage.fillAmount = newFillValue;
        }
    }
}
