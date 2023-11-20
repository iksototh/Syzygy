using GrandLine.Core.Models;
using GrandLine.Encounters;
using GrandLine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Quests
{
    public class QuestData : ScriptableObject
    {
        public List<Quest> Quests;
        public List<string> ActiveQuests;
        public event Action OnQuestComplete;

        public QuestData()
        {
            Quests = new List<Quest>();
            ActiveQuests = new List<string>();
        }

        public void AddQuest(QuestDetails questDetails)
        {
            var quest = new Quest()
            {
                QuestInformation = questDetails,
                Objective = questDetails.Objectives.First()
            };

            switch (quest.QuestInformation?.Encounter?.Type)
            {
                case "shark":
                    quest.Encounter = new SharkEncounter(quest.Id);
                    break;
                case "rogueship":
                    quest.Encounter = new AiShipEncounter(quest.Id);
                    break;
            }

            if (quest.Encounter != null)
            {
                Quests.Add(quest);
            }
        }
    }
}