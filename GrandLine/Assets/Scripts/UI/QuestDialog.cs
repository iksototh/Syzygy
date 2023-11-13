using GrandLine.Models;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI
{
    public class QuestDialog : MonoBehaviour
    {
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Reward;
        public TextMeshProUGUI Title;
        
        public Button AcceptBtn;
        public Button CancelBtn;
        public Action AcceptAction;

        private void Awake()
        {
            AcceptBtn.onClick.AddListener(Accept);
            CancelBtn.onClick.AddListener(Cancel);
        }

        public void LoadQuest(Quest quest)
        {
            Description.text = quest.Description;
            Title.text = quest.Title;
            Reward.text = $"{quest.Reward.Amount}x {quest.Reward.Type}";
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
