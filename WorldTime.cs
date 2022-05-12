using UnityEngine;

namespace Assets
{
    public class WorldTime : MonoBehaviour
    {
        public float maxTime = 2500;
        public float offset = 10;
        public Timer timer = new Timer();

        public void Start()=> timer.SetTime(maxTime, offset);
        public void Update() =>timer.UpdateTime();      
    }
}