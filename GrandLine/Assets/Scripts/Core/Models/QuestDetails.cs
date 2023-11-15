using System;
using System.Collections.Generic;

namespace GrandLine.Core.Models
{
    [Serializable]
    public class QuestDetails
    {
        public Reward Reward;
        public string Title;
        public string Description;
        public string Id;
        public string Type;
        public List<Objective> Objectives;
        public Encounter Encounter;
    }
}