using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace GrandLine.Items
{
    public class ItemMenu : ScriptableObject
    {
        [MenuItem("GrandLine/Items/GenerateAssets")]
        static void MenuCallback()
        {
            GenerateAllItems();
        }

        private static void GenerateAllItems()
        {
            foreach (var itemAsset in Resources.LoadAll<TextAsset>("Items/"))
            {
                var itemDetails = JsonConvert.DeserializeObject<ItemData>(itemAsset.text);
                var item = Item.Create(itemDetails);
                AssetDatabase.CreateAsset(item, $"Assets/Data/Items/{item.Id}.asset");
            }
        }
    }
}