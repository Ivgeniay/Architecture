using Assets.UIFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilits.Extensions;
using static Codice.Client.Common.DiffMergeToolConfig;

namespace UIFramework
{
    public class UIMediator : MonoBehaviour
    {
        public List<UIWindow> panels;

        [SerializeField] private UIInterface uiInterface;
        [SerializeField] private UIUpperPanel upperPanels;

        private Dictionary<string, UIWindow> windows = new Dictionary<string, UIWindow>();

        private void Awake()
        {
            panels = uiInterface.GetComponentsInChildren<UIWindow>(true).ToList();

            panels.ForEach(x =>
            {
                x.SetMediator(this);
                windows.Add(x.GetPath(), x);
                
                var upperPanel = Instantiate(upperPanels, x.transform);
            });

            if (windows.Count > 0)
            {
                foreach (var el in windows)
                {
                    Debug.Log(el.Key);
                }
            }
        }
    }

}
