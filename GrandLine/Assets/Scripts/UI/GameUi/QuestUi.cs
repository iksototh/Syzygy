using GrandLine.Core.Models;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GrandLine.UI.GameUi
{
    public class QuestUi : MonoBehaviour
    {
        public GameObject Quest1;
        public GameObject Panel;

        private Dictionary<string, GameObject> _activeQuests = new Dictionary<string, GameObject>();

        private TextMeshProUGUI _quest1Title;
        private TextMeshProUGUI _quest1Text;

        private void Awake()
        {
            Quest1.SetActive(false);
            var texts = Quest1.GetComponentsInChildren<TextMeshProUGUI>();
            _quest1Title = texts[0];
            _quest1Text = texts[1];
            
            Panel.SetActive(false);
        }

        public void AddQuest(Quest quest)
        {
            _activeQuests.Add(quest.Id, Quest1);

            _quest1Title.text = quest.Title;
            _quest1Text.text = quest.Objectives.First().Description;

            Quest1.SetActive(true);
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