using System;
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
        
        public static void SetPadding(this RectTransform rectTransform, RectOffset padding, bool doOverrideSafety = false)
        {
            if (!doOverrideSafety)
            {
                // Check if anchors are stretched in both directions
                if (!ZMethods.IsSameFloatValue(rectTransform.anchorMin.x, 0f) || !ZMethods.IsSameFloatValue(rectTransform.anchorMax.x, 1f) ||
                    !ZMethods.IsSameFloatValue(rectTransform.anchorMin.y, 0f) || !ZMethods.IsSameFloatValue(rectTransform.anchorMax.y, 1f))
                {
                    $"RectTransform {rectTransform.name} is not stretched in all directions. Returning.".Log(level: ZMethodsDebug.LogLevel.Warning);
                    return;
                }
            }

            // TODO should I use the new DoSafeUiModification method here instead?
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
        
        public static void DoSafeUiModification(this RectTransform rectTransform, Action action)
        {
            if (rectTransform.IsNull()) return;
            DoSafeUiModification(action);
        }
        private static void DoSafeUiModification(Action action)
        {
#if UNITY_EDITOR 
            // Delay to avoid "Can't call from inside Awake or Validate" warnings
            EditorApplication.delayCall += () =>

#endif
            {
                if (Application.isPlaying) Canvas.willRenderCanvases += OnCanvasRender;
                else action?.Invoke();
            };
            
            return;
            void OnCanvasRender()
            {
                Canvas.willRenderCanvases -= OnCanvasRender;
                action?.Invoke();
            }
        }
        
        /// <summary>
        /// Honestly I think just two colors are needed...
        /// Default: For "Normal", "Highlighted" which means hovered, "Selected" (scrollbar never needs to be "selected")
        /// Highlighted: When handle is "Pressed", or value is changed via scroll wheel (scroll wheel interaction is stupid by default and needs to be implemented manually)
        /// Disabled: I think a scrollbar will always be hidden instead of disabled
        ///</summary>
        public static void SetHandleColorBlock(this Scrollbar scrollbar, Color colorDefault, Color colorHighlighted)
        {
            scrollbar.colors = scrollbar.colors.With(
                normalColor: colorDefault,
                highlightedColor: colorDefault,
                pressedColor: colorHighlighted,
                selectedColor: colorDefault
            );
        }
        
        public static void ReconfigureAllModularUI()
        {
#if UNITY_EDITOR
            // Configure all modular UI components
            IModularUIComponent[] modularUIComponents = UnityEngine.Object.FindObjectsByType(type: typeof(MonoBehaviour), sortMode: FindObjectsSortMode.None).OfType<IModularUIComponent>().ToArray();
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