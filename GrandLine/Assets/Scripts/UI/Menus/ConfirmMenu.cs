using System;
using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI.Menus
{
    public class ConfirmMenu : MonoBehaviour
    {
        public Button EnterBtn;
        public Button LeaveBtn;

        public Action EnterAction;

        void Awake()
        {
            EnterBtn.onClick.AddListener(Enter);
            LeaveBtn.onClick.AddListener(Leave);
        }

        public void Show()
        {
            Game.SceneData.Pause();
            gameObject.SetActive(true);
        }

        private void Enter()
        {
            Debug.Log("Enter");
            gameObject.SetActive(false);
            EnterAction();
        }

        private void Leave()
        {
            Game.SceneData.UnPause();
            gameObject.SetActive(false);
        }
    }
}
