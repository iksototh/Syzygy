using System;
using UnityEngine;

namespace GrandLine.Items
{
    public class Item : ScriptableObject
    {
        public string Id;
        public string Type;

        public string Name;
        public string Description;
        public Sprite Icon;

        public static Item Create(ItemData itemData)
        {
            var item = CreateInstance<Item>();
            item.Name = itemData.Name;
            item.Description = itemData.Description;
            item.Type = itemData.Type;
            item.Id = itemData.Id;
            return item;
        }
    }
}