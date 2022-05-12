using UnityEngine;

namespace Assets
{
    [System.Serializable]
    public class Timer
    {
        public  float RawTime { get; set; }
        public  float MaxTime { get; set; }
        public  float TimeCalculationOffset { get; set; }
        public  int Hours { get; set; }
        public  int Minutes { get; set; }
        public  int Seconds { get; set; }

        public  float CurrentCountDownTime { get; set; }
        public  float ResetCountDownTime { get; set; }
        public  float CountdownCalculationOffset { get; set; }
        public  bool CallEvent { get; set; }
        public  bool CountdownLoop { get; set; }

        public delegate void TimedEvent();
        public TimedEvent Event { get; set; }

        public void SetTime(float maxTime, float offset)
        {
            MaxTime = maxTime;
            TimeCalculationOffset = offset;
        }

        /// <summary>
        /// A global time that keeps updated and loops when it reached the max amount.
        /// Seconds, Minutes and Hours are calculated from this function.
        /// </summary>
        public void UpdateTime()
        {
            if (RawTime < MaxTime)
                RawTime += Time.deltaTime * TimeCalculationOffset;

            else
            {
                RawTime = 0;
                Event?.Invoke();
            }

            if (Seconds < 60)
                Seconds = Mathf.FloorToInt(RawTime);

            else
                Seconds = 0;


            Minutes = Mathf.FloorToInt(RawTime / 60);
            Hours = Mathf.FloorToInt(RawTime / 3600);
        }

        /// <summary>
        /// Sets the countdown items used when starting to initialize things.
        /// </summary>
        public void SetCountDown(float time, bool callEvent, bool loop, float offset)
        {
            CountdownCalculationOffset = offset;
            CurrentCountDownTime = time;
            CountdownLoop = loop;
            CallEvent = callEvent;

            if (CountdownLoop)
                ResetCountDownTime = CurrentCountDownTime;
            
            if (CallEvent)
                CallEvent = true;

            else
                CallEvent = false;
        }

        /// <summary>
        /// A countdown function that sets the time, allows you to call an event and loops through if needed.
        /// </summary>
        public void CountDown()
        {
            if (CurrentCountDownTime < 0)
                CurrentCountDownTime -= Time.deltaTime * CountdownCalculationOffset;

            if(CountdownLoop)
                CurrentCountDownTime = ResetCountDownTime;

            if (CallEvent)
                Event?.Invoke();
        }
    }
}