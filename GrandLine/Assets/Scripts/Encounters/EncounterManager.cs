using GrandLine.Core.Models;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class EncounterManager : MonoBehaviour
    {
        public static EncounterManager instance;

        private void Awake()
        {
            instance = this;
        }

        public void StartEncounter(Quest quest)
        {
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