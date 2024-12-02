namespace AXitUnityTemplate.Analytics.Runtime.Interface
{
    public interface ITracker
    {
        public void SetUserId(string userId);

        public void Track(IEvent eventTracker);
    }
}