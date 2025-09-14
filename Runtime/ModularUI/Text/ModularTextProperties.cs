using System;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public struct ModularTextProperties
    {
        [SerializeField] TMP_FontAsset _font;
        [SerializeField] int _fontSize;
        [SerializeField] FontStyles _fontStyle;
        [SerializeField] ModularColor _textColor;
        [SerializeField] TextAlignmentOptions _textAlignment;
        
        public void ApplyTo(TMP_Text target)
        {
            target.font = _font;
            target.fontSize = _fontSize;
            target.fontStyle = _fontStyle;
            target.color = _textColor;
            target.alignment = _textAlignment;
            
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
}