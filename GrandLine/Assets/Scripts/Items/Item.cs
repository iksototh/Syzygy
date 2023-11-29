using Newtonsoft.Json;
using System;
using UnityEngine;

namespace GrandLine.Items
{
    [Serializable]
    public class Item
    {
        public string Id;
        public string Type;

        public string Name;
        public string Description;

        public string IconPath;

        [JsonIgnore]
        public Sprite IconSprite;
    }
}