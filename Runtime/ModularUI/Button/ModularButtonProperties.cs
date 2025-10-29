using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    [Serializable]
    public class ModularButtonProperties : BaseModularUIProperty
    {
        [Serializable]
        public struct InteractionFeedbackContainer
        {
            public ButtonInteractionFeedbackSO Hover;
            public ButtonInteractionFeedbackSO Press;
            public ButtonInteractionFeedbackSO Click; // must only be non-persistent oneshot effects
            public ButtonInteractionFeedbackSO Select;
        }
        
        [SerializeField] CommonTextProperties _text;
        [SerializeField] ModularImageProperties _backImage;
        [SerializeField] UIBorderProperties _border;
        [SerializeField] InteractionFeedbackContainer _interactionFeedbacks;
        
        public CommonTextProperties Text => _text ;
        public InteractionFeedbackContainer InteractionFeedbacks => _interactionFeedbacks;

        // No Addressables are used directly by this class
        protected override Task ReloadAddressablesAssets() => Task.CompletedTask;

        public void ApplyTo(TMP_Text text, Image backImage, Image borderImage, RectTransform buttonVisualsRectTransform, float tweenTime = 0f, Ease ease = Ease.OutQuad)
        {
            _text.ApplyTo(text, tweenTime, ease);
            _backImage.ApplyTo(backImage, tweenTime, ease);
            _border.ApplyTo(borderImage, buttonVisualsRectTransform, tweenTime, ease);
        }
    }
}