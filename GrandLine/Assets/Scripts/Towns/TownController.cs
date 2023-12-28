using GrandLine.Events;
using GrandLine.UI;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
            
            if(tag == "StartTown")
            {
                UIManager.Instance.ConfirmMenu.EnterAction = OnEnter;
                UIManager.Instance.ConfirmMenu.Show();
            }
            else
            {
                EventManager.TriggerEvent(EventTypes.QuestLoad, new BaseEventArgs());
            }
        }

        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.Instance.SavegameManager.Save();
            SceneManager.LoadScene("Hideout");
        }
    }
}