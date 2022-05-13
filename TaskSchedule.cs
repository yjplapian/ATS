using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    [CreateAssetMenu]
    public class TaskSchedule : ScriptableObject
    {
        public int taskIndex;
        public List<Task> tasks = new List<Task>();

        public Task nextTask;
        public Task currentTask;

        public bool sortOnTime;

        public void OnValidate()
        {

            if (sortOnTime)
                tasks.Sort((a, b) => a.startAt.CompareTo(b.startAt));

            if(tasks.Count >= 2)
            nextTask = tasks[1];

            currentTask = tasks[0];
            taskIndex = 1;

            tasks[0].isFirst = true;

            for (int i = 1; i < tasks.Count; i++)           
                tasks[i].isFirst = false;

        }

        public void SetNextTask()
        {
            if (taskIndex < tasks.Count -1)
                taskIndex++;

            else
                taskIndex = 0;

            currentTask = nextTask;
            nextTask = tasks[taskIndex];
        }

        public void ResetTasks()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].IsCompleted = false;
                tasks[i].IsPerforming = false;
            }
        }    
    }
}