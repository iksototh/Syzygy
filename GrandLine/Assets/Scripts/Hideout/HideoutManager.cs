using Cinemachine;
using GrandLine.Core;
using GrandLine.World;
using UnityEngine;

namespace GrandLine.Assets.Scripts.Hideout
{
    public class HideoutManager : MonoBehaviour
    {
        public static HideoutManager Instance { get; private set; }

        public GameObject Player;

        private void Awake()
        {
            Instance = this;
            
            // Game.Instance.GameData = SceneData;
            //Game.WorldMap = new WorldMap(WorldGrid);
            //Game.SavegameManager = new Systems.Savegame.SavegameManager();
            //Game.SavegameManager.Load();

            var _player = Instantiate(Player, new Vector3(-0.5f, 0.5f), Quaternion.identity);
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = _player.transform;
            cinematicScript.LookAt = _player.transform;
        }
    }
}