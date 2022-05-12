using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets
{
    [CreateAssetMenu]
    public class Task : ScriptableObject
    {
        public bool isFirst;
        public float startAt;
        public float endAt;
        public bool containsAction;
        public string action;
        public Vector3 position;

        public bool IsPerforming { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        

        void Awake()
        {
        
        }

        public void OnValidate()
        {
            ResetTask();
        }

        public void ResetTask()
        {
            IsPerforming = false;
            IsCompleted = false;
        }

        public void SetDestination(AIMotor motor, Vector3 position) 
        {
            motor.Animator.SetBool("isMoving", true);
            motor.Agent.SetDestination(position);
        }

        public void PerformAction(Animator animator, float crossfade) => animator.CrossFade(action, crossfade);
    }
}