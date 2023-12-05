using GrandLine.Events;

namespace GrandLine.Assets.Scripts.Farming
{
    public class PlantHarvestedEventArgs : IEventArgs
    {
        public string Type;
        public int Amount;
    }
}