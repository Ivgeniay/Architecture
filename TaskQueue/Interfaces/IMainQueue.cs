using System;

namespace TaskQueues
{
    public interface IMainQueue
    {
        abstract void AddTask(Action callback, TaskPriority priority = TaskPriority.Default);
        abstract void StopCurrentTask();
        public bool isBusy{ get;}
        public int Count{ get;}
        
    }

    public enum TaskPriority
    {
        Low,
        Default,
        High
    }
}
