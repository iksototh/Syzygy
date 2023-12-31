using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine.UI.Menus
{
    public class CombatMenu : MonoBehaviour
    {
        public Button ResumeBtn;
        public Button QuitBtn;
        public Button FleeBtn;

        public Canvas PauseMenuCanvas;


        void Start()
        {
            //if (!Game.Instance.SceneData.IsPaused)
            //{
            //    PauseMenuCanvas.gameObject.SetActive(false);
            //}

            ResumeBtn.onClick.AddListener(OnPauseOrResumeHandler);
            QuitBtn.onClick.AddListener(OnQuitHandler);
            FleeBtn.onClick.AddListener(OnFleeHandler);
        }

        private void OnQuitHandler()
        {
            Game.Instance.CombatManager.SceneData.Continue = false;

            SceneManager.LoadScene("Start");
        }

        private void OnPauseOrResumeHandler()
        {
            //if (Game.Instance.SceneData.IsPaused)
            //{
            //    PauseMenuCanvas.gameObject.SetActive(false);
            //    Game.Instance.SceneData.UnPause();
            //}
            //else
            //{
            //    PauseMenuCanvas.gameObject.SetActive(true);
            //    Game.Instance.SceneData.Pause();
            //}
        }

        private void OnFleeHandler()
        {
            // Game.CombatManager.SceneData.Continue = true;
            SceneManager.LoadScene("World");
        }

        void Update()
        {
            if (Keyboard.current?.escapeKey.wasReleasedThisFrame == true)
            {
                OnPauseOrResumeHandler();
            }
        }
    }
}
