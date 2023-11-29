using GrandLine.Events;
using GrandLine.Quests;
using GrandLine.ResourceSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI.GameUi
{
    public class ResourceUi : MonoBehaviour
    {
        public TextMeshProUGUI Water;
        public TextMeshProUGUI Food;
        public TextMeshProUGUI Wood;
        public TextMeshProUGUI Metal;
        public TextMeshProUGUI Scrap;
        public TextMeshProUGUI Dirt;
        public TextMeshProUGUI Fuel;
        public TextMeshProUGUI Shinies;

        void Start()
        {
            EventManager.AddListener(EventTypes.ResourceChanged, OnResourceChanged);
        }

        private void OnResourceChanged(IEventArgs args)
        {
            var eventArgs = args as ResourceEventArgs;
            switch (eventArgs.Resource.Item.Type)
            {
                case "water":
                    Water.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "food":
                    Food.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "metal":
                    Metal.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "wood":
                    Wood.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "scrap":
                    Scrap.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "dirt":
                    Dirt.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "fuel":
                    Fuel.text = eventArgs.Resource.Amount.ToString();
                    break;
                case "shinies":
                    Shinies.text = eventArgs.Resource.Amount.ToString();
                    break;
            }
        }
    }
}