using GrandLine.Assets.Scripts.Farming;
using GrandLine.Events;
using GrandLine.Interaction;
using GrandLine.ResourceSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Farming
{
    public class Plant : MonoBehaviour, IInteractable
    {
        public int GrowSpeed = 2;
        public List<SpriteRenderer> GrowStages;
        public int RotTime = 3;

        private int _growStage = 0;
        private int _growTime = 0;

        private SpriteRenderer _currentSprite;
        private string _id;

        void Awake()
        {
            _id = Guid.NewGuid().ToString();
            EventManager.AddListener(EventTypes.DaytimeHour, _id, OnHourTimerEvent);
            GrowStages[0].gameObject.SetActive(true);
        }

        void OnHourTimerEvent(IEventArgs args)
        {
            _growTime++;
            
            if (_growTime > RotTime)
            {
                Rot();
                return;
            }

            if(_growTime > GrowSpeed && _growStage < GrowStages.Count - 1)
            {
                _growStage++;
                Grow();
                _growTime = 0;
            }
        }

        private void Rot()
        {
            Destroy(gameObject);
        }

        private void Grow()
        {
            if(_growStage > 1) _currentSprite.gameObject.SetActive(false);

            _currentSprite = GrowStages[_growStage];
            _currentSprite.gameObject.SetActive(true);
        }

        private void Harvest()
        {
            Debug.Log("Plant was picked");
            EventManager.TriggerEvent(EventTypes.PlantHarvested, new PlantHarvestedEventArgs()
            {
                Type = "food",
                Amount = 1
            });
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(EventTypes.DaytimeHour, _id);
        }

        public void OnInteract()
        {
            Harvest();
        }
    }
}