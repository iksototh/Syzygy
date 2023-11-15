using GrandLine.Core.Models;
using GrandLine.Encounters;
using System;

namespace GrandLine.Quests
{
    public class Quest
    {
        public QuestDetails QuestInformation;
        public Objective Objective;
        public Guid Id => new Guid(QuestInformation.Id);
        public bool Completed;
        public IEncounter Encounter;
    }
}