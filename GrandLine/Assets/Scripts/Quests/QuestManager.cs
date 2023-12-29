using GrandLine.Core.Models;
using GrandLine.Encounters;
using GrandLine.Events;
using GrandLine.UI;
using System.Linq;
using UnityEngine;

namespace GrandLine.Quests
{
    public class QuestManager
    {
        private QuestStore _questStore;


        private QuestManager()
        {
            _questStore = QuestStore.Create();

            EventManager.AddListener(EventTypes.QuestLoad, OnQuestLoad);
            EventManager.AddListener(EventTypes.QuestAccepted, OnQuestAccepted);
            EventManager.AddListener(EventTypes.EncounterCompleted, OnEncounterCompleted);
        }

        public static QuestManager Create()
        {
            return new QuestManager();
        }

        public object Save()
        {
            return _questStore.Export();
        }

        public void Load(object data)
        {
            _questStore.Import(data);
        }


        public Quest GetRandomQuest(string questType)
        {
            var quests = _questStore.GetAvailableQuestsOfType(questType).ToList();
            
            if (quests.Count == 0) return null;

            var randomIndex = Random.Range(0, quests.Count);

            return quests[randomIndex];
        }

        private void OnQuestLoad(IEventArgs args)
        {
            if (_questStore.GetActiveQuests().ToList().Count > 0)
            {
                Debug.Log("Only two quest at a time");
                return;
            }

            var quest = GetRandomQuest("town");
            if (quest == null)
            {
                Debug.Log("No more quests currently");
                return;
            }
            Debug.Log("Loading quest here");
            UIManager.Instance.LoadQuest(quest);
        }

        private void OnQuestAccepted(IEventArgs args)
        {
            QuestEventArgs eventArgs = args as QuestEventArgs;
            
            var quest = _questStore.GetQuestById(eventArgs.Id);

            UIManager.Instance.ActivateQuest(quest);
            
            _questStore.AcceptQuest(quest);

            Game.Instance.EncounterManager.StartQuestEncounter(quest);
        }

        private void OnEncounterCompleted(IEventArgs args)
        {
            var eventArgs = args as EncounterEventArgs;
            if (string.IsNullOrEmpty(eventArgs.QuestId))
            {
                return;
            }

            CompleteQuest(eventArgs.QuestId);
        }

        private void CompleteQuest(string questId)
        {
            UIManager.Instance.QuestUi.RemoveQuest(questId);
            var quest = _questStore.GetQuestById(questId);

            _questStore.CompleteQuest(quest);

            EventManager.TriggerEvent(EventTypes.QuestCompleted, new QuestCompletedArgs()
            {
                Reward = quest.Reward,
                Id = questId
            });
        }
    }
}