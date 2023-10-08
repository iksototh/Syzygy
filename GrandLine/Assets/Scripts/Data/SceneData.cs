using UnityEngine;

namespace GrandLine.Data
{
    [CreateAssetMenu]
    public class SceneData : ScriptableObject
    {
        public bool Continue = false;
        private bool _paused = false;

        public bool IsPaused => _paused;

        public void Pause() 
        {
            Time.timeScale = 0f;
            _paused = true;
        }

        public void UnPause()
        {
            Time.timeScale = 1.0f;
            _paused = false;
        }
    }
}