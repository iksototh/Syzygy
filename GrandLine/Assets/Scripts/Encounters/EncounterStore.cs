using GrandLine.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class EncounterStore
    {
        private List<Encounter> _encounters;
        private List<Encounter> _activeEncounters;

        private EncounterStore() 
        {
            _encounters = new List<Encounter>();
            _activeEncounters = new List<Encounter>();
        }

        public static EncounterStore Create()
        {
            var encounterStore = new EncounterStore();
            encounterStore.LoadAllEncounters();
            return encounterStore;
        }

        private void LoadAllEncounters()
        {
            var encounters = Resources.LoadAll<TextAsset>("Encounters/");
            foreach (var encounterAsset in encounters)
            {
                var encounter = JsonConvert.DeserializeObject<Encounter>(encounterAsset.text);
                _encounters.Add(encounter);
            }
        }

        public Encounter GetEncounterById(string encounterId)
        {
            return _encounters.FirstOrDefault(encounter => encounter.Id == encounterId);
        }

        public List<Encounter> GetEncountersOfType(string type)
        {
            return _encounters.Where(encounter => encounter.Type == type).ToList();
        }
    }
}