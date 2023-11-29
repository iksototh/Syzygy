using GrandLine.Events;
using GrandLine.Quests;
using UnityEngine;

namespace GrandLine.ResourceSystem
{
    public class ResourceManager : MonoBehaviour
    {
        private ResourceStore _resourceStore;

        public static ResourceManager instance;

        private void Awake()
        {
            _resourceStore = new ResourceStore();
            instance = this;
        }

        public object Save()
        {
            return _resourceStore.Serialize();
        }

        public void Load(object data)
        {
            _resourceStore.CreateStore(data);
        }

        private void Start()
        {
            EventManager.AddListener(EventTypes.QuestCompleted, OnQuestCompleted);
        }

        private void OnQuestCompleted(IEventArgs args)
        {
            var eventArgs = args as QuestCompletedArgs;
            _resourceStore.AddResource(eventArgs.Reward.Type, eventArgs.Reward.Amount);
        }
    }
}