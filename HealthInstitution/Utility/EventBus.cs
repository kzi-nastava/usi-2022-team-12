using System;
using System.Collections.Generic;

namespace HealthInstitution.Utility
{
    public static class EventBus
    {
        private static readonly IDictionary<string, IList<Action>> _eventHandlers;
        private static readonly IDictionary<string, IList<Action<object>>> _eventHandlersWithParameters;

        static EventBus()
        {
            _eventHandlers = new Dictionary<string, IList<Action>>();
            _eventHandlersWithParameters = new Dictionary<string, IList<Action<object>>>();
        }

        public static void RegisterHandler(string eventName, Action handler)
        {
            if (!_eventHandlers.TryGetValue(eventName, out var handlers))
            {
                _eventHandlers[eventName] = new List<Action>();
                handlers = _eventHandlers[eventName];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(string eventName)
        {
            if (!_eventHandlers.ContainsKey(eventName)) return;

            foreach (var handler in _eventHandlers[eventName])
            {
                handler.Invoke();
            }
        }

        public static void RegisterHandler(string eventName, Action<object> handler)
        {
            if (!_eventHandlersWithParameters.TryGetValue(eventName, out var handlers))
            {
                _eventHandlersWithParameters[eventName] = new List<Action<object>>();
                handlers = _eventHandlersWithParameters[eventName];
            }
            handlers.Add(handler);
        }

        public static void FireEvent(string eventName, object parameter)
        {
            if (!_eventHandlersWithParameters.ContainsKey(eventName)) return;

            foreach (var handler in _eventHandlersWithParameters[eventName])
            {
                handler.Invoke(parameter);
            }
        }

        public static void Clear()
        {
            _eventHandlers.Clear();
            _eventHandlersWithParameters.Clear();
        }
    }
}
