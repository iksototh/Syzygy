using GrandLine.Events;

namespace GrandLine.DayCycle
{
    public class DayCycleEventArgs : IEventArgs
    {
        public string Id;
        public int Hour;
    }
}