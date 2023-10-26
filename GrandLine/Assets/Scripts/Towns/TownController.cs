using GrandLine.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour, ITrigger
    {

        // Use this for initialization
        void Start()
        {

        }

        public void OnTrigger()
        {
            //Game.GameManager.CombatData.CombatType = Core.Enums.CombatTypes.Hazard;
            //Game.GameManager.CombatData.InitiatorTile = Game.WorldMap.WorldToCell(_rigidbody2D.position).ToBaseTile();
            //Game.GameManager.CombatData.EncounterId = EncounterId;
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Town");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
        }
    }
}