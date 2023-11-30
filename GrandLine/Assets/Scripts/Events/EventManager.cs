using GrandLine.ResourceSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Events
{
    public class EventManager : MonoBehaviour
    {
        private static Dictionary<EventTypes, List<Action<IEventArgs>>> _events = new Dictionary<EventTypes, List<Action<IEventArgs>>>();

        public static void AddListener(EventTypes eventType, Action<IEventArgs> action)
        {
            if (_events.TryGetValue(eventType, out List<Action<IEventArgs>> actions))
            {
                actions.Add(action);
                return;
            }

            _events.Add(eventType, new List<Action<IEventArgs>>() { action });
        }

        public static void TriggerEvent(EventTypes eventType, IEventArgs args)
        {
            if (_events.TryGetValue(eventType, out List<Action<IEventArgs>> actions))
            {
                foreach (Action<IEventArgs> action in actions) { action?.Invoke(args); }
            }
        }
    }
}