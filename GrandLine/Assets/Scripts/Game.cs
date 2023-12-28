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
        // Can we move these?
        public Grid WorldGrid;
        public GameObject Player;
        public Vector3Int SpawnPoint;

        public static Game Instance { get; private set; }

        public SavegameManager SavegameManager;
        public CombatManager CombatManager;
        public TownManager TownManager;
        internal EncounterManager EncounterManager;
        internal QuestManager QuestManager;
        public ItemManager ItemManager;
        public ResourceManager ResourceManager;

        internal IMap WorldMap;
        internal GameData GameData;

        private void Awake()
        {
            Instance = this;

            Init();
        }

        private void Init()
        {
            GameData = ScriptableObject.CreateInstance<GameData>();
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
