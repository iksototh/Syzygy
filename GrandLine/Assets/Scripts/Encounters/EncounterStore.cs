using GrandLine.Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Encounters
{
    [CreateAssetMenu(menuName = "Encounters/Encounter Store")]
    public class EncounterStore : ScriptableObject
    {
        public List<EncounterBase> Encounters;

        private List<Encounter> _encounters;
        private List<Encounter> _activeEncounters;

        private EncounterStore() 
        {
            _encounters = new List<Encounter>();
            _activeEncounters = new List<Encounter>();
        }

        public void Init()
        {
            LoadAllEncounters();
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

        public List<EncounterBase> GetEncountersOfType(string type)
        {
            return Encounters.Where(encounter => encounter.EncounterType == type).ToList();
        }
    }
}