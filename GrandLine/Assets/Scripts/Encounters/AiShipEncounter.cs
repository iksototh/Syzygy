using UnityEngine;

namespace GrandLine.Encounters
{
    public class AiShipEncounter : IEncounter
    {
        public void Accept(System.Action complete)
        {
            Debug.Log("AI Encounter");
            
            var sharkObject = Object.Instantiate(Game.GameManager._sharkPrefab, new UnityEngine.Vector3(-15, -10), Quaternion.identity);
            var sharkScript = sharkObject.GetComponent<SharkAttack>();
            sharkScript.Complete = complete;
        }
    }
}