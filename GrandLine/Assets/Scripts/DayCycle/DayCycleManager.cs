using GrandLine.Events;
using UnityEngine;

namespace GrandLine.DayCycle
{
    public enum DayCycleSpeeds
    {
        Pause,
        Slow,
        Normal,
        Fast,
        Fastest
    }
    public class DayCycleManager : MonoBehaviour
    {
        public static DayCycleManager Instance { get; private set; }

        private int _hour;
        private float _defaultPace = 1;
        private float _hourPace;

        public void SetDaySpeed(DayCycleSpeeds speed)
        {
            switch (speed)
            {
                case DayCycleSpeeds.Slow:
                    _defaultPace = 18;
                    break;
                case DayCycleSpeeds.Normal:
                    _defaultPace = 12;
                    break;
                case DayCycleSpeeds.Fast:
                    _defaultPace = 9;
                    break;
                case DayCycleSpeeds.Fastest:
                    _defaultPace = 6;
                    break;
            }
        }

        private void Awake()
        {
            Instance = this;
            _hourPace = _defaultPace;
        }

        private void FixedUpdate()
        {
            if(_hourPace > 0)
            {
                _hourPace -= Time.deltaTime;
            } else
            {
                if(_hour > 23) _hour = 0;
                _hour++;
                EventManager.TriggerEvent(EventTypes.DaytimeHour, new DayCycleEventArgs() { Id = "hour", Hour = _hour });
                _hourPace = _defaultPace;
            }
        }
    }
}
