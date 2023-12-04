using GrandLine.DayCycle;
using GrandLine.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GrandLine.Farming
{
    public class Growable : MonoBehaviour
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

        private void OnDestroy()
        {
            EventManager.RemoveListener(EventTypes.DaytimeHour, _id);
        }
    }
}