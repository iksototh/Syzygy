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
            //Debug.Log("Trigger town");
            //EventManager.TriggerEvent(EventTypes.QuestLoad, new BaseEventArgs());
            UIManager.Instance.ConfirmMenu.EnterAction = OnEnter;
            UIManager.Instance.ConfirmMenu.Show();
        }

        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Hideout");
        }
    }
}