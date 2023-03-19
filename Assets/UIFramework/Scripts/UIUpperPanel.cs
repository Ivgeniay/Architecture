using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFramework
{
    public class UIUpperPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private Canvas canvas;

        [SerializeField] private Transform parentPanel;
        [SerializeField] private RectTransform parentRectTransform;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();

            parentPanel = rectTransform.parent;
            parentRectTransform = parentPanel.GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData) {
            parentPanel.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) {
            parentRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

        }

        public void Cross() {
            parentPanel.gameObject.SetActive(false);
        }

    }
}

