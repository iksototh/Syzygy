using GrandLine.Assets.Scripts.Managers;
using GrandLine.Encounters;
using System;
using System.Collections;
using UnityEngine;

namespace GrandLine.Encounters
{
    public class SharkEncounter : IEncounter
    {
        public void Accept(Action complete)
        {
            Complete = complete;
            SpawnShark();
        }

        private Action Complete;
        private void SpawnShark()
        {
            Debug.Log("Spawn shark");
            //var townCell = Game.WorldMap.WorldToCell(gameObject.transform.position);
            //var x = Random.Range(1, 10) + 0.5f;
            //var y = Random.Range(1, 10) + 0.5f;
            //x = Random.Range(0, 1) == 0 ? -x : x;
            //y = Random.Range(0, 1) == 0 ? -y : y;

            var sharkObject = UnityEngine.Object.Instantiate(Game.GameManager._sharkPrefab, new Vector3(-10, -10), Quaternion.identity);
            var sharkScript = sharkObject.GetComponent<SharkAttack>();
            sharkScript.Complete = Complete;
        }
    }
}