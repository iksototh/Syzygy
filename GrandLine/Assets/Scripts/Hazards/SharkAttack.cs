using GrandLine.Systems.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine
{
    public class SharkAttack : MonoBehaviour
    {
        public string EncounterId = "shark1";
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                Game.GameManager.CombatData.CombatType = Core.Enums.CombatTypes.Hazard;
                Game.GameManager.CombatData.InitiatorTile = Game.WorldMap.WorldToCell(_rigidbody2D.position).ToBaseTile();
                Game.GameManager.CombatData.EncounterId = EncounterId;
                Game.SavegameManager.Save();
                SceneManager.LoadScene("Combat");
            }
        }
    }
}
