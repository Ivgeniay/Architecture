using System;
using UIFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilits.Extensions;

namespace Assets.UIFramework
{
    public class UIWindow : MonoBehaviour, IBeginDragHandler
    {
        public bool isUpperPanel = false;
        private string pathInHierarchy;
        private UIMediator uIMediator; 

        private void Awake()
        {
            //rectTransform = GetComponent<RectTransform>();
        }

        private string SetPath(Transform transform)
        {
            var path = transform.name;

            var parent = transform.parent;

            if (parent != null) {
                var uiInterface = parent.GetComponent<UIInterface>();
                if (!uiInterface)
                    return path += "/" + SetPath(parent);
                else
                    return path += "/" + uiInterface.name;
            }

            return path;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            this.transform.SetAsLastSibling();
        }

        internal string GetPath()
        {
            pathInHierarchy = SetPath(transform);
            return pathInHierarchy.ReversePath();
        }

        internal void SetMediator(UIMediator uIMediator) {
            this.uIMediator = uIMediator;
        }
    }
}
