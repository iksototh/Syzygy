using GrandLine.Events;
using GrandLine.Items;
using System;
using System.Collections.Generic;

namespace GrandLine.ResourceSystem
{
    [Serializable]
    public class ResourceStore
    {
        private Dictionary<string, ResourceItem> _resources = new Dictionary<string, ResourceItem>();

        public void AddResource(string type, int amount)
        {
            var item = ItemManager.instance.GetItemOfType(type);
            ResourceItem resource;
            if (_resources.TryGetValue(type, out resource))
            {
                resource.Amount += amount;
            } else
            {
                resource = new ResourceItem() { Amount = amount, Item = item };
                _resources.Add(type, resource);
            }

            EventManager.TriggerEvent(EventTypes.ResourceChanged, new ResourceEventArgs() { Resource = resource });
        }
    }
}