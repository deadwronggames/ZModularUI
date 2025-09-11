using System.Globalization;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using TMPro;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public class ModularText : BaseModularUIComponent<ModularTextConfig>
    {
        [SerializeField] ModularUITheme.FontType _fontType;
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
            if (_contentFile != null) _content = _contentFile.text;
            
            string contentString = (!string.IsNullOrEmpty(_content)) ? _content : ModularTextConfig.GetDefaultText(_componentTier);
            _text.text =  ZMethods.FormattedInspectorString(contentString); 
            _text.font = _theme.GetFont(_fontType);
            _text.fontSize = _theme.GetFontSize(_componentTier);
            _text.color = _theme.GetFontColor(_componentTier);
            _text.fontStyle = _theme.GetFontStyle(_componentTier);
            _text.horizontalAlignment = _theme.GetTextAlignment(_componentTier);
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