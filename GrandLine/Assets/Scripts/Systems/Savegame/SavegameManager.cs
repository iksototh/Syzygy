using System;
using System.Collections.Generic;
using System.IO;
using Unity.Serialization.Json;
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

        public void OnSave()
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
            var file = Path.Combine(Application.persistentDataPath, "savefile.save");

            File.WriteAllText(file, JsonSerialization.ToJson(gameState));
        }

        public void OnLoad()
        {
            var file = Path.Combine(Application.persistentDataPath, "savefile.save");
            var saveData = File.ReadAllText(file);
            var gameState = JsonSerialization.FromJson<GameState>(saveData);

            Game.GameManager.LoadGame(gameState);
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