using GrandLine.Core.Models;
using GrandLine.ResourceSystem;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Quests
{
    public class QuestStore
    {
        private enum QuestLists
        {
            Completed,
            Active
        }

        private List<Quest> _allQuests;
        private Dictionary<QuestLists, List<Quest>> _questDict;
        
        public QuestStore()
        {
            _allQuests = new List<Quest>();
            _questDict = new Dictionary<QuestLists, List<Quest>>
            {
                { QuestLists.Active, new List<Quest>() },
                { QuestLists.Completed, new List<Quest>() }
            };
        }

        public static QuestStore Create()
        {
            var questStore = new QuestStore();
            questStore.LoadAllQuests();
            return questStore;
        }

        public object Export()
        {
            return _questDict;
        }

        public void Import(object data)
        {
            _questDict.Clear();
            var quests = JsonConvert.DeserializeObject<Dictionary<QuestLists, List<Quest>>>(data.ToString());
            if (quests != null)
            {
                foreach (var quest in quests)
                {
                    if(quest.Key == QuestLists.Active)
                    {
                        _questDict[QuestLists.Active] = quest.Value;
                    }

                        
                    if(quest.Key == QuestLists.Completed)
                        _questDict[QuestLists.Completed] = quest.Value;
                }
            }
        }

        public void LoadAllQuests()
        {
            foreach (var questAsset in Resources.LoadAll<TextAsset>("Quests/"))
            {
                var quest = JsonConvert.DeserializeObject<Quest>(questAsset.text);
                _allQuests.Add(quest);
            }
        }

        public Quest GetQuestById(string questId)
        {
            return _allQuests.FirstOrDefault(quest => quest.Id == questId);
        }

        public IEnumerable<Quest> GetAvailableQuestsOfType(string type)
        {
            return _allQuests.Where(quest => quest.Type == type && !GetCompletedQuests().Contains(quest) && !GetActiveQuests().Contains(quest));
        }

        public IEnumerable<Quest> GetActiveQuests()
        {
            return _questDict[QuestLists.Active];
        }

        public IEnumerable<Quest> GetCompletedQuests()
        {
            return _questDict[QuestLists.Completed];
        }

        public void CompleteQuest(string questId)
        {
            var quest = GetQuestById(questId);
            CompleteQuest(quest);
        }

        public void CompleteQuest(Quest quest)
        {
            _questDict[QuestLists.Completed].Add(quest);
            _questDict[QuestLists.Active].Remove(quest);
        }

        public void AcceptQuest(Quest quest)
        {
            _questDict[QuestLists.Active].Add(quest);
        }
    }
}