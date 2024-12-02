namespace AXitUnityTemplate.Analytics.Runtime.Installers
{
    using System;
    using UnityEngine;
    using System.Linq;
    using System.Reflection;
    using Object = UnityEngine.Object;
    using AXitUnityTemplate.Analytics.Runtime.Interface;

    public class AnalyticServiceMonoInstaller : MonoBehaviour
    {
        public bool isPersistentAcrossScenes = true;

        private AnalyticsService analyticsService;

        private void Awake()
        {
            if (this.isPersistentAcrossScenes) Object.DontDestroyOnLoad(this.gameObject);
            var types        = Assembly.GetExecutingAssembly().GetTypes();
            var trackerTypes = types.Where(t => typeof(ITracker).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            var trackerList  = trackerTypes.Select(t => (ITracker)Activator.CreateInstance(t)).ToList();
            this.analyticsService = new AnalyticsService(trackerList);
            this.analyticsService.Initialize();
        }

        private void OnDestroy() { this.analyticsService.Dispose(); }
    }
}