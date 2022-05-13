using UnityEngine;

namespace Assets
{
    [CreateAssetMenu]
    public class Task : ScriptableObject
    {
        public ITask task;

        public float startAt;
        public bool isFirst;
        public bool containsAction;
        public bool callInterface;

        public string action;
        public Vector3 position;

        public bool IsPerforming { get; set; } = false;
        public bool IsCompleted { get; set; } = false;

        public void OnValidate() => ResetTask();
        
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

        public void PerformAction(Animator animator, float crossfade)
        {
            animator.CrossFade(action, crossfade);

            if (callInterface)
                Calltask();
        }

        public void Calltask() => task.ExecuteTask();
    }
}