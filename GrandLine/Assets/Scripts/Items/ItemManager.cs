
using UnityEngine;

namespace GrandLine.Items
{
    public class ItemManager : MonoBehaviour
    {
        private ItemStore _itemStore;

        private void Awake()
        {
            _itemStore = new ItemStore();
            _itemStore.LoadAllItems();
        }

        public Item GetItemById(string id)
        {
            return _itemStore.GetItemById(id);
        }
    }
}