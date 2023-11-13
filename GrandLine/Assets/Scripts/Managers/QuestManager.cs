using GrandLine.Data;
using GrandLine.Models;
using Newtonsoft.Json;
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
            questData.Quests = new List<Quest>();
            foreach (var questAsset in Resources.LoadAll<TextAsset>("Quests/"))
            {
                var quest = JsonConvert.DeserializeObject<Quest>(questAsset.text);
                questData.AddQuest(quest);
            }
            AssetDatabase.CreateAsset(questData, "Assets/Data/Quests.asset");
            QuestData = questData;
        }

        public static Quest GetRandomQuest(string questType)
        {
            var randomIndex = Random.Range(0, QuestData.Quests.Where(quest => quest.Type == questType).Count());
            return QuestData.Quests[randomIndex];
        }
    }
}