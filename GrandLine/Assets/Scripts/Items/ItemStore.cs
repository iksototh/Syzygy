using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GrandLine.Items
{
    public class ItemStore
    {
        private List<Item> _items;

        public ItemStore()
        {
            _items = new List<Item>();
        }

        public void LoadAllItems()
        {
            foreach (var itemAsset in Resources.LoadAll<TextAsset>("Items/"))
            {
                var itemDetails = JsonConvert.DeserializeObject<Item>(itemAsset.text);
                if(!string.IsNullOrEmpty(itemDetails.IconPath))
                {
                    itemDetails.IconSprite = Resources.Load<Sprite>(itemDetails.IconPath);
                }
                _items.Add(itemDetails);
            }
        }

        public Item GetItemById(string id)
        {
            return _items.First(item => item.Id == id);
        }

        public Item GetItemOfType(string type) 
        {
            return _items.First(item => item.Type == type);
        }
    }
}