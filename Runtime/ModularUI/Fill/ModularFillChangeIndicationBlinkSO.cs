using System;
using DeadWrongGames.ZUtils;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ModularFillChangeEffectBlink", fileName = "ModularFillChangeEffectBlink")]
    public class ModularFillChangeEffectBlinkSO : BaseModularFillChangeEffectSO
    {
        /// <summary>
        /// Plays a blinking fill change effect on a modular UI component. 
        /// The primary Image updates immediately. After an initial delay,
        /// the blinking image then toggles on/off several times before
        /// it then also updates.
        /// </summary>
        [SerializeField] [Range(0f, 2f)] float _blinkDelaySeconds = 0.6f;
        [SerializeField] [Range(0, 10)] [Tooltip("Offs and ons")] int _numberBlinks = 4;
        [SerializeField] [Range(0.05f, 1f)] [Tooltip("Time for one blink")] float _blinkFrequencySeconds = 0.2f;

        public override void Play(Image mainFill, Image changeIndicator, float oldFillValue, float newFillValue, bool doTweening, int tweenId, bool useUnscaledTime, Action onCompleteAction)
        {
            bool isIncrease = (newFillValue > oldFillValue);
            Image imageFirst = (isIncrease) ? changeIndicator : mainFill;
            Image imageSecond = (isIncrease) ? mainFill : changeIndicator;
            
            // Update first image
            PlayChange(imageFirst, newFillValue, doTweening, tweenId, useUnscaledTime, onCompleteAction);

            // Blink sequence
            Sequence blinkSequence = DOTween.Sequence().SetId(tweenId);
            
            // --- Delay
            blinkSequence.AppendInterval(_blinkDelaySeconds);
            
            // --- Blink
            float halfFrequency = _blinkFrequencySeconds / 2f;
            for (int i = 0; i < _numberBlinks; i++)
            {
                blinkSequence.AppendCallback(() => changeIndicator.enabled = false);
                blinkSequence.AppendInterval(halfFrequency);
                blinkSequence.AppendCallback(() => changeIndicator.enabled = true);
                blinkSequence.AppendInterval(halfFrequency);
            }
            
            // --- Update second image
            blinkSequence.AppendCallback(() => PlayChange(imageSecond, newFillValue, doTweening, tweenId, useUnscaledTime));
            
            // --- Make sure, the blinking change indicator always ends up enabled
            blinkSequence.OnKill(() => changeIndicator.enabled = true);
            
            blinkSequence.Play();
        }
    }
}
