using System;

namespace GrandLine.Models
{
    [Serializable]
    public class Quest
    {
        public Reward Reward;
        public string Title;
        public string Description;
        public string Id;
        public string Type;
        public bool Completed;
    }
}