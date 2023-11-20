using System;
using UnityEngine;

namespace GrandLine.Items
{
    [Serializable]
    public class ItemData
    {
        public string Id;
        public string Type;

        public string Name;
        public string Description;

        public Item ToScriptableObject()
        {
            var item = ScriptableObject.CreateInstance<Item>();
            item.Name = Name;
            item.Description = Description;
            item.Type = Type;
            item.Id = Id;
            return item;
        }
    }
}