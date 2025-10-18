using System.Globalization;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public class ModularText : BaseModularUIComponent<ModularTextConfigSO>
    {
        [SerializeField] Tier _componentTier;
        [SerializeField] TextAsset _contentFile;
        [SerializeField] string _content;
      
        private TMP_Text _text;
        
        protected override void Setup()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        protected override void Apply()
        {
            // first check for content file, then string set in inspector, else default string
            if (_contentFile != null) _content = _contentFile.text;
            if (string.IsNullOrEmpty(_content)) _content = ModularTextConfigSO.GetDefaultText(_componentTier);
            _text.text =  ZMethodsString.FormattedInspectorString(_content); 
            
            // apply text properties
            ModularTextProperties properties = _theme.GetTextProperties(_componentTier);
            properties.ApplyTo(_text);
        }
        
        public string GetText() => EnsureConfiguredAndRun(() => _text.text);
        public float GetPreferredWidth() => EnsureConfiguredAndRun(() => _text.preferredWidth);

        public void SetText(float newText) => EnsureConfiguredAndRun(() => SetText(newText.ToString(CultureInfo.InvariantCulture)));
        public void SetText(string newText)
        {
            EnsureConfigured();
                
            _content = newText;
            _text.text = newText; 
        }
    }
}