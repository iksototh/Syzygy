using GrandLine.Core;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Combat
{
    public class CombatManager : MonoBehaviour
    {
        public GameData SceneData;
        public Grid WorldGrid;
        public GameObject Player;
        public GameObject Shark;

        private void Awake()
        {
            //Game.CombatManager = this;
            Game.Instance.GameData = SceneData;
            //Game.WorldMap = new WorldMap(WorldGrid);

            Instantiate(Player, new Vector3(0.5f, -0.5f), Quaternion.identity);

            Instantiate(Shark, new Vector3(5.5f, 9), Quaternion.identity);
            Instantiate(Shark, new Vector3(-6.5f, 9), Quaternion.identity);
            Instantiate(Shark, new Vector3(-6.5f, -7), Quaternion.identity);
            Instantiate(Shark, new Vector3(5.5f, -7), Quaternion.identity);
        }
    }
}
