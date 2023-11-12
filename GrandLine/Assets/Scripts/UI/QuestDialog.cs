using System;
using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI
{
    public class QuestDialog : MonoBehaviour
    {
        public Button AcceptBtn;
        public Button CancelBtn;
        public Action AcceptAction;

        private void Awake()
        {
            AcceptBtn.onClick.AddListener(Accept);
            CancelBtn.onClick.AddListener(Cancel);
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            Game.SceneData.Pause();
        }

        private void OnDisable()
        {
            Game.SceneData.UnPause();
        }

        private void Accept()
        {
            AcceptAction();
            gameObject.SetActive(false);
        }

        private void Cancel()
        {
            Debug.Log("Cancel");
            gameObject.SetActive(false);
        }
    }
}
