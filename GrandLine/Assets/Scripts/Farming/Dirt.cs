using GrandLine.Interaction;
using GrandLine.ResourceSystem;
using UnityEngine;

namespace GrandLine.Farming
{
    public class Dirt : MonoBehaviour, IInteractable
    {
        public GameObject Plant;

        private void Start()
        {
            ResourceManager.Instance.AddResourceAmount("dirt", -1);
        }

        public void OnInteract()
        {
            Debug.Log("Planted");
            Instantiate(Plant, transform);
        }

        public void OnPlant(GameObject plant)
        {
            Debug.Log("Plant something");
            Plant = plant;
        }
    }
}
