using GrandLine.Encounters;
using System;
using UnityEngine;

namespace GrandLine
{
    public class SharkAttack : MonoBehaviour, IEncounter
    {
        public string EncounterId = "shark1";
        public string RelatedQuestId;
        
        private Rigidbody2D _rigidbody2D;

        public Action OnCompleted { get; set; }

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                Debug.Log("Completed");
                //Game.GameManager.CombatData.CombatType = Core.Enums.CombatTypes.Hazard;
                //Game.GameManager.CombatData.InitiatorTile = Game.WorldMap.WorldToCell(_rigidbody2D.position).ToBaseTile();
                //Game.GameManager.CombatData.EncounterId = EncounterId;
                //Game.SavegameManager.Save();
                //SceneManager.LoadScene("Combat");
                OnCompleted();
                Destroy(gameObject);
            }
        }
    }
}
