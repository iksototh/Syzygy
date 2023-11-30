
using UnityEngine;

namespace GrandLine.Items
{
    public class ItemManager : MonoBehaviour
    {
        private ItemStore _itemStore;

        public static ItemManager instance;

        private void Awake()
        {
            _itemStore = ItemStore.Create();

            instance = this;
        }

        public Item GetItemById(string id)
        {
            return _itemStore.GetItemById(id);
        }

        public Item GetItemOfType(string type)
        {
            return _itemStore.GetItemOfType(type);
        }
    }
}