using System.Collections.Generic;
using UnityEngine;
using Utilits.Extensions;

namespace Utilits.TimeWork.Stopwatchs
{
    internal class StopwatchGrobal : MonoBehaviour
    {
        private List<Stopwatch> stopwatchList = new();
        private List<Stopwatch> removeList = new();


        private static StopwatchGrobal instance;
        public static StopwatchGrobal Instance { 
            get { 
                if (instance == null) {
                    var go = new GameObject("[STOPWATCH]");
                    instance = go.AddComponent<StopwatchGrobal>();
                    DontDestroyOnLoad(go);
                }
                return instance; 
            } 
        }


        public void RegisterStopwatch(Stopwatch stopwatch) {
            if (!stopwatchList.Contains(stopwatch)) 
                stopwatchList.Add(stopwatch);
        }

        public void UnregisterStopwatch(Stopwatch stopwatch) { 
            if (stopwatchList.Contains(stopwatch)) {
                removeList.Add(stopwatch);
            }
        }

        private void Update()
        {
            if (stopwatchList.Count == 0) return;
            
            stopwatchList.ForEach(el => {
                if (el is null) return;
                el.Tick(Time.deltaTime);
            });

            if (removeList.Count > 0) {
                removeList.ForEach(el => {
                    stopwatchList.Remove(el);
                });
                removeList.Clear();
            }
            

        }
    }
}
