using GrandLine.Data;
using GrandLine.Models;
using GrandLine.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GrandLine.Assets.Scripts.Managers
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
            var randomIndex = UnityEngine.Random.Range(0, QuestData.Quests.Where(quest => quest.QuestInformation.Type == questType && !quest.Completed).Count());
            return QuestData.Quests[randomIndex];
        }

        // Add town guid in future for list of possible town quests
        public static void LoadTownQuest()
        {
            if (QuestData.ActiveQuests.Any()) 
            {
                Debug.Log("Only one quest at a time");
                return;
            }
            var questDialog = Game.GameManager.QuestDialog.GetComponent<QuestDialog>();
            var quest = GetRandomQuest("town");
            questDialog.LoadQuest(quest);
        }

        public static QuestDetails GetQuestDetails(Guid questId) 
        { 
            var quest = QuestData.Quests.First(quest => quest.Id == questId);
            return quest.QuestInformation;
        }

        public static void AcceptQuest(Guid questId)
        {
            QuestData.ActiveQuests.Add(questId);
        }

        public static void CompleteActiveQuest()
        {
            CompleteQuest(QuestData.Quests.First().Id);
        }

        public static void CompleteQuest(Guid questId)
        {
            var questDetails = GetQuestDetails(questId);
            Debug.Log($"Completed quest! Reward: {questDetails.Reward.Amount}x {questDetails.Reward.Type}");
            QuestData.ActiveQuests.Remove(questId);
        }

        public static void CancelQuest(Guid questId)
        {
            QuestData.ActiveQuests.Remove(questId);
        }
    }
}