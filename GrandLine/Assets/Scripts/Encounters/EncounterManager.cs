using GrandLine.Core.Models;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager Instance { get; private set; }

        private EncounterStore _encounterStore;

        private void Awake()
        {
            Instance = this;
            _encounterStore = EncounterStore.Create();
        }

        public void StartEncounter(Quest quest)
        {
            var encounters = _encounterStore.GetEncountersOfType(quest.Encounter.Type);
            
            switch (quest.Encounter.Type)
            {
                case "shark":
                    new SharkEncounter(quest.Id);
                    break;
                case "rogueship":
                    new AiShipEncounter(quest.Id);
                    break;
            }
        }
    }
}