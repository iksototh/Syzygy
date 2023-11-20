using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GrandLine.Items
{
    public class ItemStore
    {
        public List<Item> _items;

        public ItemStore()
        {
            _items = new List<Item>();
        }

        public void LoadAllItems()
        {
            _items = Resources.LoadAll<Item>("Assets/Data/Items/").ToList();
        }

        public Item GetItemById(string id)
        {
            return _items.First(item => item.Id == id);
        }
    }
}