using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class UIBorderProperties
    {
        [SerializeField] Sprite _sprite;
        [SerializeField] ModularColorSO _imageColor;
        [SerializeField] RectOffset _contentPadding;
        
        public Sprite Sprite => _sprite;
        public ModularColorSO ImageColor => _imageColor;
        public RectOffset ContentPadding => _contentPadding;
        
        public void ApplyTo(Image target, RectTransform targetPadding, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            target.sprite = _sprite;
            if (tweenTime == 0f) target.color = _imageColor;
            else
            {
                DOTween.Kill(target);
                DOTween.To(getter: () => target.color, setter: c => target.color = c, endValue: _imageColor, duration: tweenTime).SetEase(ease);
            }
            
            targetPadding.SetPadding(_contentPadding);
            
            target.gameObject.SetActive(_sprite != null);
        }
    }
}