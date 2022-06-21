using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TaskQueues
{
    public sealed class MainQueue : IMainQueue
    {
        public MainQueue()
        {
            onActionIsDoneEvent += ActionIsDone;
            QueueLowPriority = new Queue<Action>();
            QueueDefaultPriority = new Queue<Action>();
            QueueHighPriority = new Queue<Action>();
        }


        public Action<object, string> onActionIsDoneEvent;
        private Queue<Action> QueueLowPriority;
        private Queue<Action> QueueDefaultPriority;
        private Queue<Action> QueueHighPriority;

        private bool _isBusy;
        public bool isBusy {
            get {
                return _isBusy;
            }
            private set {
                _isBusy = value;
            }
        }
        private int _count;
        public int Count{
            get {
                if (isBusy) 
                {
                    _count =    QueueLowPriority.Count +
                                QueueDefaultPriority.Count +
                                QueueHighPriority.Count + 1;
                }
                else 
                    _count =    0;
                return _count;
            }
        }

        public void AddTask (Action callback, TaskPriority priority = TaskPriority.Default)
        {
            SortTaskAndAddToQueue(callback, priority);
            Debug.Log($"{callback} was added with priority: {priority}");

            if (!isBusy)
            {
                isBusy = true;
                NextTask(callback);
            }
        }

        private void NextTask (Action callback)
        {

            callback?.Invoke();
            //ActionIsDone(this, "Yo");
        }
        

        private void ActionIsDone(object sender, string msg)
        {
            Debug.Log($"Task is done! MSG: {msg}, SENDER: {sender}");
            var nextTask = TaskExtraction();
            if (nextTask == null) 
            {
                isBusy = false;
                return;
            }
            NextTask(nextTask);
        }

        public void StopCurrentTask ()
        {
            throw new System.NotImplementedException();
        }

        private void SortTaskAndAddToQueue (Action callback, TaskPriority priority)
        {
            if (priority == TaskPriority.Low) QueueLowPriority.Enqueue(callback);
            if (priority == TaskPriority.Default) QueueDefaultPriority.Enqueue(callback);
            if (priority == TaskPriority.High) QueueHighPriority.Enqueue(callback);
        }


        private Action TaskExtraction ()
        {
            if (QueueHighPriority.Count != 0) 
            {
                return QueueHighPriority.Dequeue();
            }
            else if (QueueDefaultPriority.Count != 0) 
            {
                return QueueDefaultPriority.Dequeue();
            }
            else if (QueueLowPriority.Count !=0) 
            {
                return QueueLowPriority.Dequeue();
            }
            else return null;
        }
    }
}