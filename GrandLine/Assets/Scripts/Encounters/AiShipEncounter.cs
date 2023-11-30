using UnityEngine;

namespace GrandLine.Encounters
{
    public class AiShipEncounter : IEncounter
    {
        private string _relatedQuestId;

        public AiShipEncounter() { }
        public AiShipEncounter(string relatedQuestId)
        {
            _relatedQuestId = relatedQuestId;
            Accept();
        }

        public void Accept()
        {
            Debug.Log("AI Encounter");
            
            var sharkObject = Object.Instantiate(Game.GameManager._sharkPrefab, new UnityEngine.Vector3(-15, -10), Quaternion.identity);
            var sharkScript = sharkObject.GetComponent<SharkAttack>();
            sharkScript.RelatedQuestId = _relatedQuestId;
        }
    }
}