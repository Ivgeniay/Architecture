using System;

namespace Utilits.TimeWork.Stopwatchs
{
    public class Stopwatch
    {
        private float defaultStopTime;
        private float stopTime;
        private bool isWorking;

        private Action finishCallBack;
        private Action<float> tickCallBack;

        public Stopwatch(float stopTime, Action finishCallBack, Action<float> tickCallBack = null, bool isPlayOnAwake = false) {
            ConfiguringStopwatch(stopTime, finishCallBack, tickCallBack, isPlayOnAwake);
        }


        public void ConfiguringStopwatch(float stopTime, Action finishCallBack, Action<float> tickCallBack = null, bool isPlayOnAwake = false) {
            isWorking = isPlayOnAwake;

            this.stopTime = stopTime;
            this.defaultStopTime = stopTime;

            this.finishCallBack = finishCallBack;
            this.tickCallBack = tickCallBack;

            StopwatchGrobal.Instance.RegisterStopwatch(this);

            if (stopTime <= 0f) {
                Stop();
            }
        }

        public void Start() { 
            isWorking = true;
        }

        public void Pause() {
            isWorking = false;
        }
        public void Restart() {
            stopTime = defaultStopTime;
            isWorking = true;
            StopwatchGrobal.Instance.RegisterStopwatch(this);
        }

        public void Stop() {
            isWorking= false;

            tickCallBack?.Invoke(stopTime);
            finishCallBack?.Invoke();

            StopwatchGrobal.Instance.UnregisterStopwatch(this);
        }

        public void Tick(float tickTime) {
            if (isWorking) {
                stopTime -= tickTime;

                if (stopTime <= 0f) {
                    stopTime = 0;
                    Stop();
                    return;
                }

                tickCallBack?.Invoke(stopTime);
            }
        }
    }
}
