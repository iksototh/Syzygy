using GrandLine.Encounters;
using System;
using System.Collections.Generic;

namespace GrandLine.Models
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

    public class Quest
    {
        public QuestDetails QuestInformation;
        public Objective Objective;
        public Guid Id => new Guid(QuestInformation.Id);
        public bool Completed;
        public IEncounter Encounter;
    }
}