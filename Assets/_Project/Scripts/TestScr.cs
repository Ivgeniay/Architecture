using System;
using UnityEngine;
using Utilits.TimeWork.Stopwatchs;

namespace Assets.MainProject.Scripts
{
    internal class TestScr : MonoBehaviour
    {
        private Stopwatch stopwatch;

        private void Awake()
        {
            for(int i = 0; i < 10; i++)
            {
                var stopwatch = new Stopwatch(100);
                stopwatch.ConfiguringStopwatch(10, finish, tick);
                stopwatch.Start();

            }
        }

        private void tick(float obj)
        {
            Debug.Log(obj);
        }

        private void finish()
        {
            Debug.Log("Finish");
        }

        public void Method()
        {

        }
    }
}

 