using GrandLine.Core.Models;
using GrandLine.UI;
using GrandLine.UI.Dialogs;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GrandLine.Quests
{
    public class QuestManager : MonoBehaviour
    {
        private static QuestData QuestData;

        void Awake()
        {
            var questData = ScriptableObject.CreateInstance<QuestData>();
            foreach (var questAsset in Resources.LoadAll<TextAsset>("Quests/"))
            {
                var questDetails = JsonConvert.DeserializeObject<QuestDetails>(questAsset.text);
                questData.AddQuest(questDetails);
            }
            AssetDatabase.CreateAsset(questData, "Assets/Data/Quests.asset");
            QuestData = questData;
        }

        public static Quest GetRandomQuest(string questType)
        {
            var questList = QuestData
                .Quests
                .Where(quest => quest.QuestInformation.Type == questType && !quest.Completed && !QuestData.ActiveQuests.Exists(activeQuest => activeQuest == quest.Id))
                .ToList();
            if (questList.Count == 0) return null;

            var randomIndex = UnityEngine.Random.Range(0, questList.Count);

            return questList[randomIndex];
        }

        // Add town guid in future for list of possible town quests
        public static void LoadTownQuest()
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

            var questDialog = Game.GameManager.QuestDialog.GetComponent<QuestDialog>();

            questDialog.LoadQuest(quest);
        }

        public static QuestDetails GetQuestDetails(Guid questId)
        {
            var quest = QuestData.Quests.First(quest => quest.Id == questId);
            return quest.QuestInformation;
        }

        public static Quest GetQuest(Guid questId)
        {
            var quest = QuestData.Quests.First(quest => quest.Id == questId);
            return quest;
        }

        public static void AcceptQuest(Guid questId)
        {
            UIManager.QuestUi.AddQuest(GetQuest(questId));
            QuestData.ActiveQuests.Add(questId);
        }

        public static void CompleteQuest(Guid questId)
        {
            UIManager.QuestUi.RemoveQuest(questId);
            var questDetails = GetQuestDetails(questId);
            Debug.Log($"Completed quest! Reward: {questDetails.Reward.Amount}x {questDetails.Reward.Type}");
            QuestData.ActiveQuests.Remove(questId);
            if (!QuestData.ActiveQuests.Any())
            {
                UIManager.QuestUi.gameObject.SetActive(false);
            }
        }

        public static void CancelQuest(Guid questId)
        {
            QuestData.ActiveQuests.Remove(questId);
        }
    }
}