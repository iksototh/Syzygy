using GrandLine.Data;
using UnityEngine;

namespace GrandLine.Managers
{
    public class CombatManager : MonoBehaviour
    {
        public SceneData SceneData;

        private void Awake()
        {
            Game.CombatManager = this;
            Game.SceneData = SceneData;
        }
    }
}
