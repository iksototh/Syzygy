using GrandLine.Core.Models;
using GrandLine.Events;
using GrandLine.Quests;
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

        private string questId;

        private void Awake()
        {
            AcceptBtn.onClick.AddListener(Accept);
            CancelBtn.onClick.AddListener(Cancel);
        }

        public void LoadQuest(Quest quest)
        {
            questId = quest.Id;
            Description.text = quest.Description;
            Title.text = quest.Title;
            Reward.text = $"{quest.Reward.Amount}x {quest.Reward.Type}";
            gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            Game.Instance?.GameData?.Pause();
        }

        private void OnDisable()
        {
            Game.Instance?.GameData?.UnPause();
        }

        private void Accept()
        {
            EventManager.TriggerEvent(EventTypes.QuestAccepted, new QuestEventArgs() { Id = questId });
            gameObject.SetActive(false);
        }

        private void Cancel()
        {
            EventManager.TriggerEvent(EventTypes.QuestRejected, new QuestEventArgs() { Id = questId });
            gameObject.SetActive(false);
        }
    }
}
