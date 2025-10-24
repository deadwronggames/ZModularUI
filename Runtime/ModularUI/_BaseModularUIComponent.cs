using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public abstract class BaseModularUIComponent<TConfig> : MonoBehaviour, IModularUIComponent where TConfig : BaseModularUIComponentConfigSO
    {
        [Header("Config")]
        [SerializeField] [Required] protected TConfig _config;
        [SerializeField] bool _overrideProperties;

        protected ModularUIThemeSO _theme => _config.Theme;
        
        private bool _isConfigured;
        
        private void Awake()
        {
            Configure();
        }

        private void OnValidate()
        {
            Configure();
        }
        
        public void Configure()
        {
            Setup();
            if (!_overrideProperties) Apply();
            
            _isConfigured = true;
        }
        
        protected void EnsureConfiguredAndRun(Action action)
        {
            EnsureConfigured();
            action();
        }
        protected T EnsureConfiguredAndRun<T>(Func<T> func)
        {
            EnsureConfigured();
            return func();
        }
        protected void EnsureConfigured()
        {
            if (!_isConfigured) Configure();
        }
        
        protected abstract void Setup();
        protected abstract void Apply();
        
#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll()
        {
            // Configure();
            ModularUIHelpers.ReconfigureAllModularUI();
        }
#endif
    }
}