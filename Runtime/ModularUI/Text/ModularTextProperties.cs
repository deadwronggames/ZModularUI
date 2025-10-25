using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularTextProperties : CommonTextProperties
    {
        [SerializeField] FontStyles _fontStyle;
        [SerializeField] TextAlignmentOptions _textAlignment;
        
        public override void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            if (!ApplyToInternal(target, tweenTime, ease)) return;
            
            target.fontStyle = _fontStyle;
            target.alignment = _textAlignment;
        }
    }
}