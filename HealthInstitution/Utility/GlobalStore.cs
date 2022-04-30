﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInstitution.Utility
{
    public static class GlobalStore
    {
        private static readonly Dictionary<string, object?> _storedObjects;

        static GlobalStore()
        {
            _storedObjects = new Dictionary<string, object?>();
        }

        public static void AddObject(string key, object? obj)
        {
            if (_storedObjects.ContainsKey(key))
            {
                _storedObjects[key] = obj;
                return;
            }

            _storedObjects.Add(key, obj);
        }

        public static T? ReadObject<T>(string key)
        {
            return (T?)_storedObjects[key];
        }
    }
}
