#if FIREBASE_ANALYTICS && UNITY_WEBGL
namespace AXitUnityTemplate.Analytics.Runtime.Trackers.FirebaseAnalyticTracker.WebGL
{
    using System.Linq;
    using UnityEngine;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using AXitUnityTemplate.Analytics.Runtime.Interface;

    public class FirebaseAnalyticTrackerWebGL : ITracker
    {
        public void SetUserId(string userId) { FirebaseAnalyticTrackerWebGL.SetIdUser(userId); }

        public void Track(IEvent eventTracker)
        {
            if (!eventTracker.Name.IsNameValid(out var message))
            {
                Debug.LogError(message);
                return;
            }

            if (eventTracker.Data == null || eventTracker.Data.Count == 0)
            {
                FirebaseAnalyticTrackerWebGL.LogEventWithoutParameters(eventTracker.Name);
            }
            else if (eventTracker.Data.Count == 1)
            {
                var (key, value) = eventTracker.Data.First();
                FirebaseAnalyticTrackerWebGL.LogParameter(eventTracker.Name, key, value);
            }
            else
            {
                FirebaseAnalyticTrackerWebGL.LogMultipleParameters(eventTracker.Name, eventTracker.Data);
            }
        }
        
        private static void LogParameter(string eventName, string key, object value)
        {
            var valueAsString = value switch
            {
                long or int or double or float or string => value.ToString(),
                _                                        => JsonConvert.SerializeObject(value),
            };

            FirebaseAnalyticTrackerWebGL.LogEventWithSingleParameter(eventName, key, valueAsString);
        }

        private static void LogMultipleParameters(string eventName, Dictionary<string, object> data)
        {
            if (data == null || data.Count == 0)
            {
                Debug.LogError("LogMultipleParameters: Data is null or empty, no event will be logged.");
                return;
            }

            var eventParameter = JsonConvert.SerializeObject(data);
            FirebaseAnalyticTrackerWebGL.LogEventWithMultipleParameters(eventName, eventParameter);
        }
        
        [DllImport("__Internal")]
        private static extern void SetIdUser(string id);

        [DllImport("__Internal")]
        private static extern void LogEventWithSingleParameter(string eventName, string parameterKey, string parameterValue);

        [DllImport("__Internal")]
        private static extern void LogEventWithMultipleParameters(string eventName, string eventParameter);

        [DllImport("__Internal")]
        private static extern void LogEventWithoutParameters(string eventName);
    }
}
#endif
