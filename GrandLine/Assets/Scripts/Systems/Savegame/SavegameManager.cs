using GrandLine.Quests;
using GrandLine.ResourceSystem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GrandLine.Systems.Savegame
{
    public class SavegameManager
    {
        private List<Func<ShipState>> SavablesFuncs;
        
        public SavegameManager() 
        { 
            SavablesFuncs = new List<Func<ShipState>>();
        }

        public void Save(string fileName = "savefile1.save")
        {
            var shipStates = new List<ShipState>();
            foreach (var func in SavablesFuncs)
            {
                var result = func();
                shipStates.Add(result);
            }

            var gameState = new GameState()
            {
                ships = shipStates
            };

            gameState.resourceStore = ResourceManager.Instance.Save();
            gameState.questStore = Game.Instance.QuestManager.Save();
            Game.Instance.GameData.GameState = gameState;
            var file = Path.Combine(Application.persistentDataPath, fileName);
            var jsonData = JsonConvert.SerializeObject(gameState, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            File.WriteAllText(file, jsonData);
            Debug.Log("saved");
        }

        public void Load(string fileName = "savefile1.save")
        {
            var file = Path.Combine(Application.persistentDataPath, fileName);
            var saveData = File.ReadAllText(file);
            var gameState = JsonConvert.DeserializeObject<GameState>(saveData);
            ResourceManager.Instance.Load(gameState.resourceStore);
            Game.Instance.QuestManager.Load(gameState.questStore);
            Game.Instance.GameData.GameState = gameState;
            // Game.GameManager.LoadGame(gameState);
            Debug.Log("loaded");
        }

        public void AddSaveable(Func<ShipState> func)
        {
            SavablesFuncs.Add(func);
        }

        public void ResetSaveableFuncs()
        {
            SavablesFuncs?.Clear();
        }
    }
}