using System;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class UIBorderProperties : ModularUIProperties<Image>
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] ModularColor _imageColor;
        [SerializeField] RectOffset _contentPadding;
        
        public override void ApplyTo(Image target)
        {
            target.sprite = _sprite;
            target.color = _imageColor;
            target.gameObject.SetActive(_sprite != null);
        }

        public void ApplyPadding(RectTransform target)
        {
            target.SetPadding(_contentPadding);
        }
    }
}