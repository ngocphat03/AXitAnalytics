#if FIREBASE_ANALYTICS && (UNITY_ANDROID || UNITY_IOS)
namespace AXitUnityTemplate.Analytics.Runtime.Trackers.FirebaseAnalyticTracker
{
    using System.Linq;
    using UnityEngine;
    using Newtonsoft.Json;
    using Firebase.Analytics;
    using System.Collections.Generic;
    using AXitUnityTemplate.Analytics.Runtime.Interface;

    public class FirebaseAnalyticTracker : ITracker
    {
        public void SetUserId(string userId) { FirebaseAnalytics.SetUserId(userId); }

        public void Track(IEvent eventTracker)
        {
            if (!eventTracker.Name.IsNameValid(out var message))
            {
                Debug.LogError(message);
                return;
            }

            if (eventTracker.Data == null || eventTracker.Data.Count == 0)
            {
                FirebaseAnalytics.LogEvent(eventTracker.Name);
            }
            else if (eventTracker.Data.Count == 1)
            {
                var (key, value) = eventTracker.Data.First();
                this.LogParameter(eventTracker.Name, key, value);
            }
            else
            {
                this.LogMultipleParameters(eventTracker.Name, eventTracker.Data);
            }
        }
        
        private void LogParameter(string eventName, string key, object value)
        {
            switch (value)
            {
                case long   l: FirebaseAnalytics.LogEvent(eventName, key, l); break;
                case int    i: FirebaseAnalytics.LogEvent(eventName, key, i); break;
                case string s: FirebaseAnalytics.LogEvent(eventName, key, s); break;
                case double d: FirebaseAnalytics.LogEvent(eventName, key, d); break;
                case float  f: FirebaseAnalytics.LogEvent(eventName, key, f); break;
                default:       FirebaseAnalytics.LogEvent(eventName, key, JsonConvert.SerializeObject(value)); break;
            }
        }
        
        private void LogMultipleParameters(string eventName, Dictionary<string, object> data)
        {
            var parameters = new List<Parameter>();

            foreach (var kvp in data)
            {
                switch (kvp.Value)
                {                    
                    case long   l: parameters.Add(new Parameter(kvp.Key, l)); break;
                    case int    i: parameters.Add(new Parameter(kvp.Key, i)); break;
                    case string s: parameters.Add(new Parameter(kvp.Key, s)); break;
                    case double d: parameters.Add(new Parameter(kvp.Key, d)); break;
                    case float  f: parameters.Add(new Parameter(kvp.Key, f)); break;
                    default:       parameters.Add(new Parameter(kvp.Key, JsonConvert.SerializeObject(kvp.Value))); break;
                }
            }

            FirebaseAnalytics.LogEvent(eventName, parameters.ToArray());
        }
    }
}
#endif
