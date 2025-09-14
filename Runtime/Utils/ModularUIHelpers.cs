using System.Collections.Generic;
using System.Linq;
using DeadWrongGames.ZCommon;
using DeadWrongGames.ZUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    public static class ModularUIHelpers
    {
        public static TProperty GetProperty<TProperty>(Tier tier, TProperty[] array)
        {
            int index = Mathf.Min((int)tier, array.Length - 1);
            return array[index];
        }
        
        public static void SetPadding(this RectTransform rectTransform, RectOffset padding)
        {
            // Check if anchors are stretched in both directions
            if (!ZMethods.IsSameFloatValue(rectTransform.anchorMin.x, 0f) || !ZMethods.IsSameFloatValue(rectTransform.anchorMax.x, 1f) ||
                !ZMethods.IsSameFloatValue(rectTransform.anchorMin.y, 0f) || !ZMethods.IsSameFloatValue(rectTransform.anchorMax.y, 1f))
            {
                $"RectTransform {rectTransform.name} is not stretched in all directions. Returning.".Log(level: ZMethodsDebug.LogLevel.Warning);
                return;
            }

#if UNITY_EDITOR
            // Delay to avoid "Can't call from inside Awake or Validate" warnings
            EditorApplication.delayCall += () =>
#endif
            {
                if (rectTransform == null) return;
                rectTransform.offsetMin = new Vector2(padding.left, padding.bottom);
                rectTransform.offsetMax = new Vector2(-padding.right, -padding.top);
            };
        }
        
        public static void ReconfigureAllModularUI()
        {
#if UNITY_EDITOR
            // Configure all modular UI components
            IModularUIComponent[] modularUIComponents = Object.FindObjectsOfType<MonoBehaviour>().OfType<IModularUIComponent>().ToArray();
            foreach (IModularUIComponent modularUIComponent in modularUIComponents)
                modularUIComponent.Configure();

            // Collect root GOs
            Transform[] rootTransforms = modularUIComponents
                .Select(component => ((MonoBehaviour)component).transform.root)
                .Distinct()
                .ToArray();

            // Force layout rebuild
            foreach (Transform transform in rootTransforms)
            {
                foreach (RectTransform rectTransform in transform.GetComponentsInChildren<RectTransform>())
                    LayoutRebuilder.MarkLayoutForRebuild(rectTransform); 
            }

            // Save scene
            if (!EditorApplication.isPlaying) 
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
#endif
        }
    }
}