using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class UIBorderProperties : ModularImageProperties
    {
        [SerializeField] RectOffset _contentPadding;
        public RectOffset ContentPadding => _contentPadding;

        // ApplyTo with either one or multiple padding targets
        public void ApplyTo(Image target, RectTransform paddingTarget, float tweenTime = 0f, Ease ease = Ease.OutQuad) => ApplyTo(target, new[] { paddingTarget }, tweenTime, ease);
        public void ApplyTo(Image target, IEnumerable<RectTransform> paddingTargets, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            // Apply base image logic
            ApplyTo(target, tweenTime, ease);
            
            // Extra step for borders: Apply padding to bordered elements
            foreach (RectTransform paddingTarget in paddingTargets)
                paddingTarget.SetPadding(_contentPadding);
        }
    }
}