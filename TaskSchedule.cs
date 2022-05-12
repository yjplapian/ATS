//using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//using UnityEditor;

namespace Assets
{
    [CreateAssetMenu]
    public class TaskSchedule : ScriptableObject
    {
        public int taskIndex;
        public List<Task> tasks = new List<Task>();

        public Task nextTask;
        public Task currentTask;
        public Task previousTask;

        public void OnValidate()
        {
            if(tasks.Count >= 2)
            nextTask = tasks[1];

            currentTask = tasks[0];
            previousTask = null;
            taskIndex = 1;
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