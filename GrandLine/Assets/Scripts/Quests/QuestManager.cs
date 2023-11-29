﻿using GrandLine.Core.Models;
using GrandLine.Encounters;
using GrandLine.Events;
using GrandLine.UI;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;

namespace GrandLine.Quests
{
    public class QuestEventArgs : IEventArgs
    {
        public string Id;
        public string Name;
    }

    public class QuestManager : MonoBehaviour
    {
        private static QuestData QuestData;

        public static QuestManager instance;

        void Awake()
        {
            var questData = ScriptableObject.CreateInstance<QuestData>();
            foreach (var questAsset in Resources.LoadAll<TextAsset>("Quests/"))
            {
                var questDetails = JsonConvert.DeserializeObject<QuestDetails>(questAsset.text);
                questData.AddQuest(questDetails);
            }
            
            QuestData = questData;
            instance = this;
        }

        private void Start()
        {
            EventManager.AddListener(EventTypes.QuestLoad, OnQuestLoad);
            EventManager.AddListener(EventTypes.QuestAccepted, OnQuestAccepted);
            EventManager.AddListener(EventTypes.EncounterCompleted, OnEncounterCompleted);
        }

        public Quest GetRandomQuest(string questType)
        {
            var questList = QuestData
                .Quests
                .Where(quest => quest.QuestInformation.Type == questType && !quest.Completed && !QuestData.ActiveQuests.Exists(activeQuest => activeQuest == quest.Id))
                .ToList();
            if (questList.Count == 0) return null;

            var randomIndex = UnityEngine.Random.Range(0, questList.Count);

            return questList[randomIndex];
        }

        private void OnQuestLoad(IEventArgs args)
        {
            if (QuestData.ActiveQuests.Count > 1)
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

            UIManager.QuestDialog.LoadQuest(quest);
        }

        public QuestDetails GetQuestDetails(string questId)
        {
            var quest = QuestData.Quests.First(quest => quest.Id == questId);
            return quest.QuestInformation;
        }

        public static Quest GetQuest(string questId)
        {
            var quest = QuestData.Quests.First(quest => quest.Id == questId);
            return quest;
        }

        public static void OnQuestAccepted(IEventArgs args)
        {
            QuestEventArgs eventArgs = args as QuestEventArgs;
            Debug.Log($"eventArgs {eventArgs.Id}");
            var quest = GetQuest(eventArgs.Id);

            UIManager.QuestUi.AddQuest(quest);
            
            QuestData.ActiveQuests.Add(quest.Id);
            quest.Encounter.Accept();
        }

        public void CompleteQuest(string questId)
        {
            UIManager.QuestUi.RemoveQuest(questId);
            var questDetails = GetQuestDetails(questId);
            
            Debug.Log($"Completed quest! Reward: {questDetails.Reward.Amount}x {questDetails.Reward.Type}");
            QuestData.ActiveQuests.Remove(questId);
            if (!QuestData.ActiveQuests.Any())
            {
                UIManager.QuestUi.gameObject.SetActive(false);
            }

            EventManager.TriggerEvent(EventTypes.QuestCompleted, new QuestCompletedArgs()
            {
                Reward = questDetails.Reward,
                Id = questId
            });
        }

        public Reward GetQuestReward(string questId)
        {
            return GetQuest(questId).QuestInformation.Reward;
        }

        public void CancelQuest(string questId)
        {
            QuestData.ActiveQuests.Remove(questId);
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
    }
}