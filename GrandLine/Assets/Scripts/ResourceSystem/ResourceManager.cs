using GrandLine.Assets.Scripts.Farming;
using GrandLine.Events;
using GrandLine.Quests;
using GrandLine.Systems.Savegame;
using UnityEngine;

namespace GrandLine.ResourceSystem
{
    public class ResourceManager : MonoBehaviour, ISave
    {
        private ResourceStore _resourceStore;

        public static ResourceManager Instance { get; private set; }

        private void Awake()
        {
            _resourceStore = ResourceStore.Create();
            Instance = this;

            EventManager.AddListener(EventTypes.QuestCompleted, OnQuestCompleted);
            EventManager.AddListener(EventTypes.PlantHarvested, OnPlantHarvested);

            
        }

        public object Save()
        {
            return _resourceStore.Export();
        }

        public void Load(object data)
        {
            _resourceStore.Import(data);
        }

        public int GetResourceAmount(string type)
        {
            return _resourceStore.GetResourceAmount(type);
        }

        public void AddResourceAmount(string type, int amount)
        {
            _resourceStore.AddResource(type, amount);
        }

        private void OnPlantHarvested(IEventArgs args) 
        {
            var resourceArgs = args as PlantHarvestedEventArgs;
            _resourceStore.AddResource(resourceArgs.Type, resourceArgs.Amount);
        }

        private void OnQuestCompleted(IEventArgs args)
        {
            var eventArgs = args as QuestCompletedArgs;
            _resourceStore.AddResource(eventArgs.Reward.Type, eventArgs.Reward.Amount);
        }
    }
}