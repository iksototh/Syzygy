using GrandLine.Core.Enums;
using GrandLine.World;
using GrandLine.Systems.Savegame;
using GrandLine.Combat;
using GrandLine.Towns;
using GrandLine.Core;
using GrandLine.Encounters;
using GrandLine.Quests;
using GrandLine.Items;
using GrandLine.ResourceSystem;

using UnityEngine;
using Cinemachine;

namespace GrandLine
{
    public class Game : MonoBehaviour
    {
        public GameData GameData; // SO for game state

        // Can we move these?
        public Grid WorldGrid;
        public GameObject Player;
        public Vector3Int SpawnPoint;

        public static Game Instance { get; private set; }

        public SavegameManager SavegameManager;
        public CombatManager CombatManager;
        public TownManager TownManager;
        public ItemManager ItemManager;
        public ResourceManager ResourceManager;

        internal EncounterManager EncounterManager;
        internal QuestManager QuestManager;
        internal IMap WorldMap;

        private void Awake()
        {
            Instance = this;
            
            Init();
        }

        private void Init()
        {
            WorldMap = new WorldMap(WorldGrid);
            SavegameManager = new SavegameManager();

            EncounterManager = EncounterManager.Create();
            QuestManager = QuestManager.Create();
        }

        private void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            var _player = Instantiate(Player, new Vector3(SpawnPoint.x + 0.5f, SpawnPoint.y + 0.5f), Quaternion.identity);
            var playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
            var cinematicScript = playerCamera.GetComponent<CinemachineVirtualCamera>();
            cinematicScript.Follow = _player.transform;
            cinematicScript.LookAt = _player.transform;
        }
    }
}
