namespace AXitUnityTemplate.Analytics.Runtime
{
    using UnityEngine;
    using System.Collections.Generic;
    using AXitUnityTemplate.Analytics.Runtime.Interface;

    public class AnalyticsService
    {
        private readonly List<ITracker> tracker;

        public AnalyticsService(List<ITracker> tracker) { this.tracker = tracker; }

        public void Initialize() { AnalyticsService.Instance = this; }

        public static AnalyticsService Instance { get; private set; }

        public void TrackEvent(IEvent eventTrack)
        {
            // Check if the event is null, or the event name is null/empty, or the event data is null
            if (eventTrack == null || string.IsNullOrEmpty(eventTrack.Name) || eventTrack.Data == null)
            {
                Debug.LogError($"Invalid event: {eventTrack?.Name ?? "Unknown"}");

                return;
            }

            // Track the event using all trackers
            this.tracker.ForEach(track => track.Track(eventTrack));
        }

        public void Dispose() { AnalyticsService.Instance = null; }
    }
}