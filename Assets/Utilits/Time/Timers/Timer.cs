using System;
using UnityEngine;

namespace Utilits.TimeWork.Timers
{
    public class Timer 
    {
        public event Action<float> OnTimerValueChangedEvent;
        public event Action OnTimerFinishedEvent;
        public TimerType TimerType { get; private set; }
        public float RemainingValue { get; private set; }
        public bool IsPaused { get; private set; }

        public Timer(TimerType timerType) {
            this.TimerType = timerType;
        }

        public Timer(TimerType timerType, float seconds) {
            this.TimerType = timerType;
            SetTimer(seconds);
        }

        public void SetTimer(float seconds) {
            this.RemainingValue = seconds;
            OnTimerValueChangedEvent?.Invoke(RemainingValue);
        }

        public void Start() { 
            if (RemainingValue == 0)
            {
                Debug.Log("Timer remainig time is 0");
                OnTimerFinishedEvent?.Invoke();
                return;
            }

            IsPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingValue);
        }

        public void Start(float time)
        {
            SetTimer(time);
            Start();
        }

        public void Pause() {
            IsPaused = true;
            Unsubscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingValue);
        }
        public void Resume() {
            IsPaused = false;
            Subscribe();
            OnTimerValueChangedEvent?.Invoke(RemainingValue);
        }
        public void Stop() { 
            Unsubscribe();
            RemainingValue = 0;

            OnTimerValueChangedEvent?.Invoke(RemainingValue);
            OnTimerFinishedEvent?.Invoke();
        }


        private void Subscribe()
        {
            switch (TimerType)
            {
                case TimerType.Scale:
                    TimerGlobal.Instance.OnUpdateTimerEvent += OnUpdateTimerHandler;
                    break;
                case TimerType.Unscale: 
                    TimerGlobal.Instance.OnUpdateTimerUnscaledEvent += OnUpdateTimerHandler;
                    break;
                case TimerType.Second:
                    TimerGlobal.Instance.OnOneSecondEvent += OnOneHandler;
                    break;
                case TimerType.UnscaleSecond: 
                    TimerGlobal.Instance.OnOneSecondUscaledEvent += OnOneHandler;
                    break;
                case TimerType.Minute:
                    TimerGlobal.Instance.OnOneMinuteEvent += OnOneHandler;
                    break;
                case TimerType.UnscaleMinute:
                    TimerGlobal.Instance.OnOneMinuteUscaledEvent += OnOneHandler;
                    break;
            }
        }
        private void Unsubscribe()
        {
            switch (TimerType)
            {
                case TimerType.Scale:
                    TimerGlobal.Instance.OnUpdateTimerEvent -= OnUpdateTimerHandler;
                    break;
                case TimerType.Unscale:
                    TimerGlobal.Instance.OnUpdateTimerUnscaledEvent -= OnUpdateTimerHandler;
                    break;
                case TimerType.Second:
                    TimerGlobal.Instance.OnOneSecondEvent -= OnOneHandler;
                    break;
                case TimerType.UnscaleSecond:
                    TimerGlobal.Instance.OnOneSecondUscaledEvent -= OnOneHandler;
                    break;
                case TimerType.Minute:
                    TimerGlobal.Instance.OnOneMinuteEvent -= OnOneHandler;
                    break;
                case TimerType.UnscaleMinute:
                    TimerGlobal.Instance.OnOneMinuteUscaledEvent -= OnOneHandler;
                    break;
            }
        }

        private void OnOneHandler() {
            if (IsPaused) return;

            RemainingValue -= 1;
            CheckTimer();
        }

        private void OnUpdateTimerHandler(float delta) {
            if (IsPaused) return;

            RemainingValue -= delta;
            CheckTimer();
        }

        private void CheckTimer()
        {
            if (RemainingValue <= 0) Stop();
            else OnTimerValueChangedEvent?.Invoke(RemainingValue);
        }
    }
}
