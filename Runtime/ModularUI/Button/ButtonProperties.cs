using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ButtonProperties
    {
        public Vector2 ButtonSize;
        public ButtonTextProperties Text;
        public ButtonImageProperties FrontImage;
        public ButtonImageProperties MiddleImage;
        public ButtonImageProperties BackImage;
        public UIBorderProperties Border;
    }
    
    [Serializable]
    public class ButtonTextProperties : ModularUIProperties<TMP_Text>
    {
        [SerializeField] TMP_FontAsset _font;
        [SerializeField] int _fontSize;
        [SerializeField] ModularColor _textColor;
        
        public override void ApplyTo(TMP_Text target)
        {
            target.font = _font;
            target.fontSize = _fontSize;
            target.color = _textColor;
            target.gameObject.SetActive((_font != null) && (_fontSize > 0));
        }
    }
    
    [Serializable]
    public class ButtonImageProperties : ModularUIProperties<Image>
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] ModularColor _imageColor;
        
        public override void ApplyTo(Image target)
        {
            target.sprite = _sprite;
            target.color = _imageColor;
            target.gameObject.SetActive(_sprite != null);
        }
    }
}