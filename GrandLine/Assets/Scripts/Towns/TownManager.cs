using Cinemachine;
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

        public TownManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            //Game.TownManager = this;
            Game.Instance.GameData = SceneData;
            //Game.WorldMap = new WorldMap(WorldGrid);

            var _player = Instantiate(Player, new Vector3(-15.5f, 5.5f), Quaternion.identity);

            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = _player.transform;
            cinematicScript.LookAt = _player.transform;
        }
    }
}