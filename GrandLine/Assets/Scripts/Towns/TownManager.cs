using GrandLine.Core;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Towns
{
    public class TownManager : MonoBehaviour
    {
        public GameData SceneData;
        public Grid WorldGrid;
        public GameObject Player;

        private void Awake()
        {
            Game.TownManager = this;
            Game.SceneData = SceneData;
            Game.WorldMap = new WorldMap(WorldGrid);

            Instantiate(Player, new Vector3(-15.5f, 5.5f), Quaternion.identity);
        }
    }
}