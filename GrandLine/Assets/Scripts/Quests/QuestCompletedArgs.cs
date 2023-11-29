using GrandLine.Core.Models;
using GrandLine.Events;

namespace GrandLine.Quests
{
    public class QuestCompletedArgs : IEventArgs
    {
        public string Id;
        public Reward Reward;
    }
}