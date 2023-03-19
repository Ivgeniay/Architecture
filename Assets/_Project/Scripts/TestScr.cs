using UnityEngine;
using Utilits.Extensions;
using Utilits.Timers;

namespace Assets.MainProject.Scripts
{

    internal class TestScr : MonoBehaviour
    {
        [SerializeField] private MainViewModel prefab;

        [SerializeField] private TimerType timerType;
        [SerializeField] private float timerRem;
        [SerializeField] private float timerScale;
        private Timer timer;


        private void Awake()
        {
            timer = new Timer(timerType, timerRem);
            timer.OnTimerValueChangedEvent += Timer_OnTimerValueChangedEvent;
            timer.OnTimerFinishedEvent += Timer_OnTimerFinishedEvent;
            timer.Start();

            Time.timeScale = timerScale;
        }

        private void Timer_OnTimerFinishedEvent()
        {
            Debug.Log("Finished");
        }

        private void Timer_OnTimerValueChangedEvent(float obj)
        {
            Debug.Log(obj);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                timer.Start();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                timer.Stop();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (timer.IsPaused)
                    timer.Resume();
                else
                    timer.Pause();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                timer.Start();
            }
        }
    }


}

 