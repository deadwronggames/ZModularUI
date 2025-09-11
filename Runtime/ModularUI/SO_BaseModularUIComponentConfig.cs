using Sirenix.OdinInspector;
using UnityEngine;

namespace DeadWrongGames.ZModularUI
{
    public abstract class BaseModularUIComponentConfig : ScriptableObject
    {
        public ModularUITheme Theme => _theme;
        [SerializeField] [Required] ModularUITheme _theme;
        
#if UNITY_EDITOR
        [Button("Reconfigure all Modular UI")]
        public void ReconfigureAll() => ModularUIHelpers.ReconfigureAllModularUI();
#endif
    }
}