using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TaskQueues
{
    public static class MQueue
    {
        public static Action<object, string> onActionIsDoneEvent;

        //private static Action<object, string> 
        private static MainQueue _instance;
        private static MainQueue instance{
            get {
                if (_instance == null)
                {
                    _instance = new MainQueue();
                    onActionIsDoneEvent = instance.onActionIsDoneEvent;// = onActionIsDoneEvent;
                }
                
                return _instance;
            }
        }

        public static void AddTask(Action callback, TaskPriority priority = TaskPriority.Default)
        {
            instance.AddTask(callback, priority);
        }

        public static int Count()
        {
            return instance.Count;
        }

        public static bool isBusy()
        {
            return instance.isBusy;
        }

    }
}
