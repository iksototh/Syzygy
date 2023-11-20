using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Events
{
    public class EventManager : MonoBehaviour
    {
        private static Dictionary<EventTypes, Action<EventArgs>> _events = new Dictionary<EventTypes, Action<EventArgs>>();


        public static void AddListener(EventTypes eventType, Action<EventArgs> action) 
        {
            _events.Add(eventType, action);
        }

        public static void TriggerEvent(EventTypes eventType, EventArgs args)
        {
            if (_events.TryGetValue(eventType, out Action<EventArgs> action))
            {
                action?.Invoke(args);
            }
        }
    }
}