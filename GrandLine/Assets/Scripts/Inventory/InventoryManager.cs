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

        public TextMeshProUGUI Water;
        public TextMeshProUGUI Food;
        public TextMeshProUGUI Wood;
        public TextMeshProUGUI Metal;
        public TextMeshProUGUI Scrap;
        public TextMeshProUGUI Dirt;
        public TextMeshProUGUI Fuel;
        public TextMeshProUGUI Shinies;

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

        private void OnQuestCompleted(EventArgs args)
        {
            var eventArgs = args as QuestCompletedArgs;
            switch(eventArgs.Reward.Type)
            {
                case "water":
                    Water.text = (int.Parse(Water.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "food":
                     Food.text = (int.Parse(Food.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "metal":
                    Metal.text = (int.Parse(Metal.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "wood":
                    Wood.text = (int.Parse(Wood.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "scrap":
                    Scrap.text = (int.Parse(Scrap.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "dirt":
                    Dirt.text = (int.Parse(Dirt.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "fuel":
                    Fuel.text = (int.Parse(Fuel.text) + eventArgs.Reward.Amount).ToString();
                    break;
                case "shinies":
                    Shinies.text = (int.Parse(Shinies.text) + eventArgs.Reward.Amount).ToString();
                    break;
            }
            Debug.Log($"{eventArgs.Reward.Amount} - {eventArgs.Reward.Type}");
        }
    }
}