using System;
using System.Collections.Generic;
using System.IO;
using Unity.Serialization.Json;
using UnityEngine;

namespace GrandLine.Systems.Savegame
{
    public class SavegameManager
    {
        private List<Func<SaveState>> SavablesFuncs;
        
        public SavegameManager() 
        { 
            SavablesFuncs = new List<Func<SaveState>>();
        }

        public void OnSave()
        {
            var gameState = new List<SaveState>();
            foreach (var func in SavablesFuncs)
            {
                var result = func();
                gameState.Add(result);
            }
            var file = Path.Combine(Application.persistentDataPath, "savefile.save");
            File.WriteAllText(file, JsonSerialization.ToJson(gameState));
        }

        public void OnLoad()
        {
            var file = Path.Combine(Application.persistentDataPath, "savefile.save");
            var saveData = File.ReadAllText(file);
            var gameState = JsonSerialization.FromJson<List<SaveState>>(saveData);
            Debug.Log(gameState);
        }

        public void AddSaveable(Func<SaveState> func)
        {
            SavablesFuncs.Add(func);
        }
    }
}