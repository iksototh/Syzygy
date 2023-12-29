using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GrandLine;
using GrandLine.Scenes;

namespace GrandLine.UI.Menus
{
    public class TownMenu : MonoBehaviour
    {
        public Button ResumeButton;
        public Button LeaveButton;
        public Button QuitBtn;
        public Canvas TownCanvas;

        void Awake()
        {
            ResumeButton.onClick.AddListener(OnPauseOrResumeHandler);
            LeaveButton.onClick.AddListener(OnLeave);
            QuitBtn.onClick.AddListener(OnQuitHandler);
        }

        private void OnPauseOrResumeHandler()
        {
            if (Game.Instance.GameData.IsPaused)
            {
                TownCanvas.gameObject.SetActive(false);
                Game.Instance.GameData.UnPause();
            }
            else
            {
                TownCanvas.gameObject.SetActive(true);
                Game.Instance.GameData.Pause();
            }
        }
        private void OnLeave()
        {
            Game.Instance.TownManager.SceneData.Continue = true;
            SceneLoader.LoadWorld();
        }

        void Start()
        {
            if (!Game.Instance.GameData.IsPaused)
            {
                TownCanvas.gameObject.SetActive(false);
            }
        }

        private void OnQuitHandler()
        {
            Game.Instance.TownManager.SceneData.Continue = false;

            SceneLoader.LoadMainMenu();
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