using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeadWrongGames.ZModularUI
{
    // Attach to viewport or background
    [RequireComponent(typeof(Image))] // Just as raycast target, should be set to transparent
    public class ScrollViewportTrigger : EventTrigger
    {
        private const float SCROLL_SENSITIVITY_FACTOR = 0.002f;
        
        private ModularScrollView _scrollView;
        private float _scrollSensitivity;

        private void Awake()
        {
            _scrollView = transform.GetComponentInParent<ModularScrollView>();
            _scrollSensitivity = _scrollView.GetComponent<ScrollRect>().scrollSensitivity;
        }

        // This ensures mouse wheel scrolling works anywhere in the viewport
        // Since non of the other event triggers are implemented (like e.g. drag) they are ignored for the attached GO 
        public override void OnScroll(PointerEventData eventData)
        {
            // Set new scrollbar value based on current value and scaled PointerEventData scroll delta
            _scrollView.ScrollbarValue = Mathf.Clamp01(_scrollView.ScrollbarValue + SCROLL_SENSITIVITY_FACTOR * _scrollSensitivity * eventData.scrollDelta.y);
        }
    }
}
