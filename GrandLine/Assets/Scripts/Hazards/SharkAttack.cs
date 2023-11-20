using GrandLine.Encounters;
using GrandLine.Events;
using GrandLine.Systems.Extensions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine
{
    public class SharkAttack : MonoBehaviour
    {
        public string EncounterId = "shark1";
        public string RelatedQuestId;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                Debug.Log("Completed");
                EventManager.TriggerEvent(EventTypes.EncounterCompleted, new EncounterEventArgs() { QuestId = RelatedQuestId });
                //Game.GameManager.CombatData.CombatType = Core.Enums.CombatTypes.Hazard;
                //Game.GameManager.CombatData.InitiatorTile = Game.WorldMap.WorldToCell(_rigidbody2D.position).ToBaseTile();
                //Game.GameManager.CombatData.EncounterId = EncounterId;
                //Game.SavegameManager.Save();
                //SceneManager.LoadScene("Combat");
                Destroy(gameObject);
            }
        }
    }
}
