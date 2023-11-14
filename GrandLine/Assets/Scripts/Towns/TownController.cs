using GrandLine.Assets.Scripts.Managers;
using GrandLine.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour, ITrigger
    {
        public void OnTrigger()
        {
            // Should add the town here
            QuestManager.LoadTownQuest();
        }

        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Town");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
        }
    }
}