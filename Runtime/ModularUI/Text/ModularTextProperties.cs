using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularTextProperties
    {
        [SerializeField] TMP_FontAsset _font;
        [SerializeField] int _fontSize;
        [SerializeField] FontStyles _fontStyle;
        [SerializeField] ModularColorSO _textColor;
        [SerializeField] TextAlignmentOptions _textAlignment;
        
        public TMP_FontAsset Font => _font;
        public int FontSize => _fontSize;
        public FontStyles FontStyle => _fontStyle;
        public ModularColorSO TextColor => _textColor;
        public TextAlignmentOptions TextAlignment => _textAlignment;
        
        public void ApplyTo(TMP_Text target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            target.font = _font;
            target.fontStyle = _fontStyle;
            target.alignment = _textAlignment;
            if (tweenTime == 0f)
            {
                target.fontSize = _fontSize;
                target.color = _textColor;
            }
            else
            {
                DOTween.Kill(target);
                DOTween.To(getter: () => target.fontSize, setter: x => target.fontSize = x, endValue: _fontSize, duration: tweenTime).SetEase(ease);
                DOTween.To(getter: () => target.color, setter: c => target.color = c, endValue: _textColor, duration: tweenTime).SetEase(ease);
            }
            
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
}