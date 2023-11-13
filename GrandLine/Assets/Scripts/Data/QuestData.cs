using GrandLine.Models;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Data
{
    [CreateAssetMenu]
    public class QuestData : ScriptableObject
    {
        public List<Quest> Quests;

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
        }
    }
}