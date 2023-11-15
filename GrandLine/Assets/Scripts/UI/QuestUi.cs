using GrandLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GrandLine.UI
{
    public class QuestUi : MonoBehaviour
    {
        public GameObject QuestList;
        public GameObject Quest;
        public GameObject Panel;

        private Dictionary<Guid, GameObject> _activeQuests = new Dictionary<Guid, GameObject>();

        public void AddQuest(Quest quest)
        {
            var questText = Quest.GetComponent<TextMeshProUGUI>();
            questText.text = $"{quest.QuestInformation.Title}\n\n{quest.Objective.Description}";
            var questGO = Instantiate(Quest, QuestList.transform);
            _activeQuests.Add(quest.Id, questGO);

            if(_activeQuests.Count > 1) 
            {
                questGO.transform.localPosition = new Vector3Int(0, -150, 0);
                Panel.GetComponent<RectTransform>().sizeDelta = new Vector2Int(300, 600);
            }

            gameObject.SetActive(true);
        }

        public void RemoveQuest(Guid questId)
        {
            var quest = _activeQuests[questId];
            _activeQuests.Remove(questId);
            Destroy(quest);

            if (_activeQuests.Count < 2 && _activeQuests.Any())
            {
                _activeQuests.First().Value.transform.localPosition = new Vector3Int(0, 100, 0);
                Panel.GetComponent<RectTransform>().sizeDelta = new Vector2Int(300, 350);
            }
        }
    }
}