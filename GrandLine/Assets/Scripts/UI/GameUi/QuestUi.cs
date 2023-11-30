using GrandLine.Core.Models;
using GrandLine.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GrandLine.UI.GameUi
{
    public class QuestUi : MonoBehaviour
    {
        public GameObject Quest1;
        public GameObject Quest2;
        public GameObject Panel;

        private Dictionary<string, GameObject> _activeQuests = new Dictionary<string, GameObject>();

        private TextMeshProUGUI _quest1Text;
        private TextMeshProUGUI _quest2Text;

        private void Awake()
        {
            Quest1.SetActive(false);
            _quest1Text = Quest1.GetComponent<TextMeshProUGUI>();
            Quest2.SetActive(false);
            _quest2Text = Quest2.GetComponent<TextMeshProUGUI>();
            Panel.SetActive(false);
        }

        public void AddQuest(Quest quest)
        {
            var questGo = _activeQuests.Count < 1 ? Quest1 : Quest2;
            var questText = _activeQuests.Count < 1 ? _quest1Text : _quest2Text;

            _activeQuests.Add(quest.Id, questGo);

            questText.text = $"{quest.Title}\n\n{quest.Objectives.First().Description}";
            
            questGo.SetActive(true);
            Panel.SetActive(true);
        }

        public void RemoveQuest(string questId)
        {
            var quest = _activeQuests[questId];
            _activeQuests.Remove(questId);
            quest.SetActive(false);
        }
    }
}