using System;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class AiShipEncounter : MonoBehaviour, IEncounter
    {
        public Action OnCompleted { get; set; }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("AI encounter triggered");
            OnCompleted();
            Destroy(gameObject);
        }
    }
}