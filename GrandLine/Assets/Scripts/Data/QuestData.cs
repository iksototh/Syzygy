using GrandLine.Encounters;
using GrandLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Data
{
    [CreateAssetMenu]
    public class QuestData : ScriptableObject
    {
        public List<Quest> Quests;
        public List<Guid> ActiveQuests;

        public QuestData() 
        { 
            Quests = new List<Quest>();
            ActiveQuests = new List<Guid>();
        }

        public void AddQuest(QuestDetails questDetails)
        {
            var quest = new Quest() { 
                QuestInformation = questDetails, 
                Objective = questDetails.Objectives.First() 
            };

            switch (quest.QuestInformation?.Encounter?.Type)
            {
                case "shark":
                    quest.Encounter = new SharkEncounter();
                    break;
                case "rogueship":
                    quest.Encounter = new AiShipEncounter();
                    break;
            }
            
            if(quest.Encounter != null)
            {
                Quests.Add(quest);
            }
        }
    }
}