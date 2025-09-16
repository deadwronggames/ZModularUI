using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ButtonTextProperties
    {
        [SerializeField] TMP_FontAsset _font;
        [SerializeField] int _fontSize;
        [SerializeField] ModularColorSO _textColor;
        
        public TMP_FontAsset Font => _font;
        public int FontSize => _fontSize;
        public ModularColorSO TextColor => _textColor;
        
        public void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            target.font = _font;
            target.fontSize = _fontSize;
            if (tweenTime == 0f) target.color = _textColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_textColor, tweenTime).SetEase(ease);
            }
            
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
}