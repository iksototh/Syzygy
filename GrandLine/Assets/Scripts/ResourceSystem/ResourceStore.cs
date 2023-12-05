using GrandLine.Events;
using GrandLine.Items;
using Mono.Cecil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrandLine.ResourceSystem
{
    public class ResourceStore
    {
        private Dictionary<string, ResourceItem> _resources = new Dictionary<string, ResourceItem>();

        public static ResourceStore Create()
        {
            return new ResourceStore();
        }

        public object Export()
        {
            return _resources;
        }

        public void Import(object data)
        {
            _resources.Clear();
            var resources = JsonConvert.DeserializeObject<Dictionary<string, ResourceItem>>(data.ToString());
            if (resources != null)
            {
                foreach (var resource in resources.Values) 
                {
                    AddResource(resource.Item.Type, resource.Amount, false);
                }
            }
        }

        public void AddResource(string type, int amount, bool add = true)
        {
            var item = ItemManager.instance.GetItemOfType(type);
            ResourceItem resource;
            if (add && _resources.TryGetValue(type, out resource))
            {
                resource.Amount += amount;
            } else
            {
                resource = new ResourceItem() { Amount = amount, Item = item };
                _resources.Add(type, resource);
            }

            EventManager.TriggerEvent(EventTypes.ResourceChanged, new ResourceEventArgs() { Resource = resource });
        }

        public int GetResourceAmount(string type)
        {
            if(_resources.TryGetValue(type, out var resource))
            {
                return resource.Amount;
            }

            return 0;
        }
    }
}