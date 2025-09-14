using System;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public struct ButtonTextProperties
    {
        [SerializeField] TMP_FontAsset _font;
        [SerializeField] int _fontSize;
        [SerializeField] ModularColor _textColor;
        
        public void ApplyTo(TMP_Text target)
        {
            target.font = _font;
            target.fontSize = _fontSize;
            target.color = _textColor;
            
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
}