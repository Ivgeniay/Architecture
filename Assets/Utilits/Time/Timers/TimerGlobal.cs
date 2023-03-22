using System;
using UnityEngine;

namespace Utilits.TimeWork.Timers
{
    internal class TimerGlobal : MonoBehaviour {
        public event Action<float> OnUpdateTimerEvent;
        public event Action<float> OnUpdateTimerUnscaledEvent;
        public event Action OnOneSecondEvent;
        public event Action OnOneSecondUscaledEvent;
        public event Action OnOneMinuteEvent;
        public event Action OnOneMinuteUscaledEvent;

        private float oneSecond;
        private float oneSecondUnscaled;
        private float oneMinute;
        private float oneMinuteUnscaled;

        private static TimerGlobal instance;
        public static TimerGlobal Instance
        {
            get { 
                if (instance == null)
                {
                    var go = new GameObject("[TIMER]");
                    instance = go.AddComponent<TimerGlobal>();
                    DontDestroyOnLoad(go);
                }
                return instance; 
            }
        }

        private void Update() {
            var delta = Time.deltaTime;

            OnUpdateTimerEvent?.Invoke(delta);

            oneSecond += delta;
            if (oneSecond >= 1) {
                oneSecond -= 1;
                OnOneSecondEvent?.Invoke();
            }

            oneMinute += delta;
            if (oneMinute >= 60) {
                oneMinute -= 60;
                OnOneMinuteEvent?.Invoke();
            }

            var unscaleDelta = Time.unscaledDeltaTime;

            OnUpdateTimerUnscaledEvent?.Invoke(unscaleDelta);

            oneSecondUnscaled += unscaleDelta;
            if (oneSecondUnscaled >= 1) {
                oneSecondUnscaled -= 1;
                OnOneSecondUscaledEvent?.Invoke();
            }

            oneMinuteUnscaled += unscaleDelta;
            if (oneMinuteUnscaled >= 60) {
                oneMinuteUnscaled -= 60;
                OnOneMinuteUscaledEvent?.Invoke();
            }
        }

    }
}
