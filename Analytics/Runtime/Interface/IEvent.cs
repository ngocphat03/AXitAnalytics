namespace AXitUnityTemplate.Analytics.Runtime.Interface
{
    using System.Collections.Generic;

    public interface IEvent
    {
        public string                     Name { get; }
        public Dictionary<string, object> Data { get; }
    }
}