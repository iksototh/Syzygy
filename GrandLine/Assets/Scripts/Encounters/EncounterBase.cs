using GrandLine.Events;
using UnityEngine;

namespace GrandLine.Encounters
{
    [CreateAssetMenu(menuName = "Encounters/Encounter")]
    public class EncounterBase : ScriptableObject
    {
        protected string _relatedQuestId;
        public GameObject EncounterPrefab;
        public Vector3 SpawnPoint;
        public string EncounterId;
        public string EncounterType;

        public void SetQuest(string relatedQuestId)
        {
            _relatedQuestId = relatedQuestId;
        }

        public void Execute()
        {
            Debug.Log($"Spawn ${EncounterType} - {EncounterId}");

            var encounterObject = Instantiate(EncounterPrefab, new Vector3(SpawnPoint.x + .5f, SpawnPoint.y + .5f), Quaternion.identity);

            var encounterScript = encounterObject.GetComponentInChildren<IEncounter>();
            encounterScript.OnCompleted = OnCompleted;
        }

        private void OnCompleted()
        {
            EventManager.TriggerEvent(EventTypes.EncounterCompleted, new EncounterEventArgs() { QuestId = _relatedQuestId });
        }
    }
}