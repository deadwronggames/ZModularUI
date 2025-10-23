using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    /// <summary>
    /// Plays a catch-up fill change effect on a modular UI component.
    /// The primary image updates immediately, while the secondary image updates after a delay.
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ModularFillChangeIndicationCatchUp", fileName = "ModularFillChangeIndicationCatchUp")]
    public class ModularFillChangeEffectCatchUpSO : BaseModularFillChangeEffectSO
    {
        [SerializeField] float _catchUpDelay = 0.8f;
        
        public override void Play(Image mainFill, Image changeIndicator, float oldFillValue, float newFillValue, bool doTweening, bool useUnscaledTime, Action onCompleteAction)
        {
            bool isIncrease = (newFillValue > oldFillValue);
            Image imageFirst = (isIncrease) ? changeIndicator : mainFill;
            Image imageSecond = (isIncrease) ? mainFill : changeIndicator;

            PlayChange(imageFirst, newFillValue, doTweening, useUnscaledTime, onCompleteAction);
            DOTween.Sequence()
                .AppendInterval(_catchUpDelay)
                .AppendCallback(() => PlayChange(imageSecond, newFillValue, doTweening, useUnscaledTime))
                .SetUpdate(useUnscaledTime);
        }
    }
}