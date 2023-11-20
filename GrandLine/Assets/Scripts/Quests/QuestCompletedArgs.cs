using GrandLine.Core.Models;
using System;

namespace GrandLine.Quests
{
    public class QuestCompletedArgs : EventArgs
    {
        public string Id;
        public Reward Reward;
    }
}