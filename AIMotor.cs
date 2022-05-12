using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets
{
    public class AIMotor : MonoBehaviour
    {
        public WorldTime GlobalTime;
        public Animator Animator { get { return GetComponent<Animator>(); } }
        public NavMeshAgent Agent { get { return GetComponent<NavMeshAgent>(); } }

        public TaskSchedule schedule;
        public List<Transform> positions;

        private void Start()
        {
            GlobalTime = FindObjectOfType<WorldTime>();

            for (int i = 0; i < schedule.tasks.Count; i++)          
                schedule.tasks[i].position = positions[i].position;

            StartCurrentTask();

            GlobalTime.timer.CallEvent = true;
            GlobalTime.timer.Event += schedule.ResetTasks;
        }

        public void StartCurrentTask() => StartCoroutine(ExecuteCurrentTask());

        public IEnumerator ExecuteCurrentTask()
        {
            if(schedule.currentTask.isFirst)
                yield return new WaitUntil(() => GlobalTime.timer.RawTime >= schedule.currentTask.startAt);

            schedule.currentTask.SetDestination(this, schedule.currentTask.position);
            yield return new WaitUntil(() => HasReachedDestination());
            Animator.SetBool("isMoving", false);

            if (schedule.currentTask.containsAction)
            {
                schedule.currentTask.PerformAction(Animator, 0.2f);
                schedule.currentTask.IsCompleted = true;

                #if UNITY_EDITOR
                Debug.Log("Found Action in: " + schedule.currentTask.name);
                #endif
            }

            else
            {
                #if UNITY_EDITOR
                Debug.Log("Found No Action in: " + schedule.currentTask.name);
                #endif
                schedule.currentTask.IsCompleted = true;
            }

            if (schedule.currentTask.IsCompleted)
                schedule.SetNextTask();

            yield return new WaitUntil(() => GlobalTime.timer.RawTime >= schedule.currentTask.startAt && !schedule.currentTask.IsCompleted);
            StartCurrentTask();
        }

        public bool HasReachedDestination()
        {
            if (!Agent.pathPending)
            {
                if (Agent.remainingDistance <= Agent.stoppingDistance)
                {
                    if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}