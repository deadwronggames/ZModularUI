using System;
using System.Collections.Generic;
using DeadWrongGames.ZUtils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ModularUI/ButtonInteractionFeedback", fileName = "ButtonInteractionFeedback")]
    public class ButtonInteractionFeedbackSO : ScriptableObject
    {
        public const float TWEEN_TIME = 0.2f;
        public const Ease TWEEN_EASE = Ease.OutQuad;
        
        // Audio
        [Tooltip("Define an audio clip to be played.")]
        [SerializeField] AudioClip _audio;
        
        // Text colors
        [Tooltip("Define which text color gets changed to what other color.")]
        [SerializeField] ColorPair[] _textColorMap = {};
        [Tooltip("Feedback color to be used when no transition is defined in the map. If this field is also not assigned, don't change text color.")]
        [SerializeField] ModularColorSO _textColorFallback;
        [Serializable] private struct ColorPair { public ModularColorSO Key; public ModularColorSO Value; }

        // Sprites
        [Tooltip("Define sprite changes. Fields left empty correspond to no change.")] 
        [SerializeField] SpriteSet _alternativeSpriteSet;
        [Serializable] private struct SpriteSet { public Sprite Front, Middle, Back; }

        public void DoFeedback(ModularButtonProperties defaultProperties, bool doOneshots, TMP_Text text, Image frontImage, Image middleImage, Image backImage)
        {
            if (doOneshots)
            {
                if (_audio != null) "TODO playing audio clip not implemented yet".Log(level: ZMethodsDebug.LogLevel.Warning);
            }

            if (TryGetNewColor(defaultProperties.Text.TextColor, _textColorMap, _textColorFallback, out ModularColorSO newTextColor))
            {
                DOTween.Kill(text);
                text.DOColor(newTextColor, TWEEN_TIME).SetId(text).SetEase(TWEEN_EASE);
            }

            if (_alternativeSpriteSet.Front != null)  frontImage.sprite  = _alternativeSpriteSet.Front;
            if (_alternativeSpriteSet.Middle != null) middleImage.sprite = _alternativeSpriteSet.Middle;
            if (_alternativeSpriteSet.Back != null)   backImage.sprite   = _alternativeSpriteSet.Back;

            // Can add more as needed
        }

        private static bool TryGetNewColor(ModularColorSO defaultColor, IEnumerable<ColorPair> colorMap, ModularColorSO colorFallback, out ModularColorSO newColor)
        {
            // Try to find new color from color map
            foreach (ColorPair colorPair in colorMap)
            {
                if (defaultColor.Color == colorPair.Key.Color)
                {
                    newColor = colorPair.Value;
                    return true;
                }
            }
       
            // otherwise fallback or just null
            newColor = colorFallback;
            return (newColor != null);
        }

    }
}