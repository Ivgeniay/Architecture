using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilits.Extensions;

namespace UIFramework
{
    public class UIMediator : MonoBehaviour
    {
        public List<UIWindow> windowList;

        [SerializeField] private UIInterface uiInterface;
        [SerializeField] private UIUpperPanel upperPanels;

        private Dictionary<string, UIWindow> windows = new Dictionary<string, UIWindow>();

        private void Awake() {
            windowList = uiInterface.GetComponentsInChildren<UIWindow>(true).ToList();

            windowList.ForEach(x => {
                x.SetMediator(this);
                windows.Add(x.GetPath(), x);
                x.With(
                    el => Instantiate(upperPanels, x.transform),
                    @if: el => el.isUpperPanel
                    );
            });
        }
    }

}
