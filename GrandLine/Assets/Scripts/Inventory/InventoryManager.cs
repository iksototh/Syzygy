using GrandLine.Events;
using GrandLine.Items;
using GrandLine.Quests;
using Newtonsoft.Json;
using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace GrandLine.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private static Inventory Inventory;

        void Awake()
        {
            //var inventoryData = ScriptableObject.CreateInstance<Inventory>();
            //foreach (var itemAsset in Resources.LoadAll<TextAsset>("Items/"))
            //{
            //    var itemDetails = JsonConvert.DeserializeObject<ItemData>(itemAsset.text);
            //    var item = itemDetails.ToScriptableObject();
            //    var item2 = Item.Create(itemDetails);
                
            //    inventoryData.AddItem(itemDetails);
            //}
            //AssetDatabase.CreateAsset(inventoryData, "Assets/Data/Inventory.asset");
            //Inventory = inventoryData;
            EventManager.AddListener(EventTypes.QuestCompleted, OnQuestCompleted);
        }

        private void OnQuestCompleted(IEventArgs args)
        {
            var eventArgs = args as QuestCompletedArgs;
            switch (eventArgs.Reward.Type)
            {
                case "item":
                    // Water.text = (int.Parse(Water.text) + eventArgs.Reward.Amount).ToString();
                    break;

            }
        }
    }
}