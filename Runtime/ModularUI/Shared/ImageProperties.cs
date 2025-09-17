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
        [SerializeField] ModularColorSO _imageColor;
        
        public Sprite Sprite => _sprite;
        public ModularColorSO ImageColor => _imageColor;
        
        public void ApplyTo(Image target, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            target.sprite = _sprite;
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                DOTween.To(getter: () => target.color, setter: c => target.color = c, endValue: _imageColor, duration: tweenTime).SetEase(ease);
            }
            
            target.gameObject.SetActive(_sprite != null);
        }
    }
}