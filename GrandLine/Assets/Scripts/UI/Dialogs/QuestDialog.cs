using GrandLine.Core.Models;
using GrandLine.Quests;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI.Dialogs
{
    public class QuestDialog : MonoBehaviour
    {
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Reward;
        public TextMeshProUGUI Title;

        public Button AcceptBtn;
        public Button CancelBtn;
        public Action AcceptAction;
        public Action CancelAction;

        private Guid questId;

        private void Awake()
        {
            AcceptBtn.onClick.AddListener(Accept);
            CancelBtn.onClick.AddListener(Cancel);
        }

        public void LoadQuest(Quest quest)
        {
            questId = new Guid(quest.QuestInformation.Id);
            Description.text = quest.QuestInformation.Description;
            Title.text = quest.QuestInformation.Title;
            Reward.text = $"{quest.QuestInformation.Reward.Amount}x {quest.QuestInformation.Reward.Type}";
            AcceptAction = () => quest.Encounter.Accept(() => QuestManager.CompleteQuest(quest.Id));
            gameObject.SetActive(true);
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
            QuestManager.AcceptQuest(questId);
            AcceptAction();
            gameObject.SetActive(false);
        }

        private void Cancel()
        {
            if (CancelAction != null) CancelAction();
            gameObject.SetActive(false);
        }
    }
}
