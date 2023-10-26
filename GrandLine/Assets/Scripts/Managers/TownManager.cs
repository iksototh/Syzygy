using GrandLine.Data;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Managers
{
    public class TownManager : MonoBehaviour
    {
        public SceneData SceneData;
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