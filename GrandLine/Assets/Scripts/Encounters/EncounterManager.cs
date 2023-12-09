using GrandLine.Core.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class EncounterManager
    {
        private EncounterStore _encounterStore;

        private EncounterManager()
        {
            _encounterStore = Resources.Load<EncounterStore>("EncounterDatabase");
            _encounterStore.Init();
        }

        public static EncounterManager Create()
        {

            return new EncounterManager();
        }

        public void StartQuestEncounter(Quest quest)
        {
            var encounter = _encounterStore.GetEncountersOfType(quest.Encounter.Type).FirstOrDefault();
            if (encounter == null)
            {
                Debug.LogWarning($"No such encounter type: {quest.Encounter.Type}");
                return;
            }

            encounter.SetQuest(quest.Id);
            encounter.Execute();
        }
    }
}