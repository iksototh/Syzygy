using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Events
{
    class EventListener
    {
        public string Id;
        public List<Action<IEventArgs>> Events;
    }

    public static class EventManager
    {
        private static Dictionary<EventTypes, List<EventListener>> _events2 = new Dictionary<EventTypes, List<EventListener>>();

        private static Dictionary<EventTypes, List<Action<IEventArgs>>> _events = new Dictionary<EventTypes, List<Action<IEventArgs>>>();


        public static void AddListener(EventTypes eventType, string id, Action<IEventArgs> action)
        {
            if (_events2.TryGetValue(eventType, out List<EventListener> listeners))
            {
                if (listeners.Any(l => l.Id == id))
                {
                    listeners.First(l => l.Id == id).Events.Add(action);
                }
                else
                {
                    listeners.Add(new EventListener() { Id = id, Events = new List<Action<IEventArgs>>() { action } });
                }

                _events2[eventType] = listeners;
                return;
            }

            _events2.Add(eventType, new List<EventListener>() { new EventListener() { Id = id, Events = new List<Action<IEventArgs>>() { action } } });
        }

        public static void AddListener(EventTypes eventType, Action<IEventArgs> action)
        {
            if (_events.TryGetValue(eventType, out List<Action<IEventArgs>> actions))
            {
                actions.Add(action);
                return;
            }

            _events.Add(eventType, new List<Action<IEventArgs>>() { action });
        }

        public static void RemoveListener(EventTypes eventType, string id)
        {
            if (_events2.TryGetValue(eventType, out List<EventListener> listeners))
            {
                listeners.RemoveAll(listener => listener.Id == id);
            }
        }

        public static void TriggerEvent(EventTypes eventType, IEventArgs args)
        {
            if (_events.TryGetValue(eventType, out List<Action<IEventArgs>> actions))
            {
                foreach (Action<IEventArgs> action in actions) { action?.Invoke(args); }
            }

            if (_events2.TryGetValue(eventType, out List<EventListener> events))
            {
                foreach (EventListener eventListener in events.ToList())
                {
                    eventListener.Events.ToList().ForEach(e => e?.Invoke(args));
                }
            }
        }
    }
}