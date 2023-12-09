using GrandLine.Events;
using System;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class SharkEncounter : MonoBehaviour, IEncounter
    {
        public Action OnCompleted { get; set; }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Shark encounter triggered");
            OnCompleted();
            Destroy(gameObject);
        }
    }
}