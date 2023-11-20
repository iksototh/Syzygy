using GrandLine.Core.Enums;
using GrandLine.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Inventory
{
    [Serializable]
    public class Inventory : ScriptableObject
    {
        public Guid Id;
        public InventoryTypes Type;

        public string Name;
        public string Description;

        public List<InventoryItem> CollectedItems = new List<InventoryItem>();

        public void AddItem(InventoryItem item)
        { 
            CollectedItems.Add(item);
        }
    }
}