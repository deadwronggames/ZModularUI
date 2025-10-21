using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class UIBorderProperties : ImageProperties
    {
        [SerializeField] RectOffset _contentPadding;
        public RectOffset ContentPadding => _contentPadding;
        
        public void ApplyTo(Image target, RectTransform targetPadding, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Apply base image logic
            ApplyTo(target, tweenTime, ease);
            
            // Extra step for borders
            targetPadding.SetPadding(_contentPadding);
        }
    }
}