using GrandLine.Events;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour
    {
        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Town");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
            EventManager.TriggerEvent(EventTypes.QuestLoad, new EventArgs());
            //UIManager.ConfirmMenu.EnterAction = OnEnter;
            //UIManager.ConfirmMenu.Show();
        }
    }
}