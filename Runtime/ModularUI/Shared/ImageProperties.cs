using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ImageProperties
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] ModularColor _imageColor;
        
        public Sprite Sprite => _sprite;
        public ModularColor ImageColor => _imageColor;
        
        public void ApplyTo(Image target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            target.sprite = _sprite;
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                target.DOColor(_imageColor, tweenTime);
            }
            
            target.gameObject.SetActive(_sprite != null);
        }
    }
}