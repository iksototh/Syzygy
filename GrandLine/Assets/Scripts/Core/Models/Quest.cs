using System;
using System.Collections.Generic;

namespace GrandLine.Core.Models
{
    [Serializable]
    public class Quest
    {
        public string Id;
        public string Type;

        public string Title;
        public string Description;
        
        public List<Objective> Objectives;
        public Reward Reward;
        public Encounter Encounter;

        public bool Equals(Quest other)
        {
            return Id == other.Id;
        }
    }
}