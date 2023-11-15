using GrandLine.Quests;
using GrandLine.Triggers;
using GrandLine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour
    {
        //public void OnTrigger()
        //{
        //    // Should add the town here
        //    //UIManager.ConfirmMenu.EnterAction = OnEnter;
        //    //UIManager.ConfirmMenu.Show();
            

        //}

        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Town");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
            QuestManager.LoadTownQuest();
        }
    }
}