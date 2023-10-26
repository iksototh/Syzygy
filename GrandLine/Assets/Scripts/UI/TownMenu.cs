using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine.UI
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
            if (Game.SceneData.IsPaused)
            {
                TownCanvas.gameObject.SetActive(false);
                Game.SceneData.UnPause();
            }
            else
            {
                TownCanvas.gameObject.SetActive(true);
                Game.SceneData.Pause();
            }
        }
        private void OnLeave() 
        {
            Game.TownManager.SceneData.Continue = true;
            SceneManager.LoadScene("World");
        }

        void Start()
        {
            if (!Game.SceneData.IsPaused)
            {
                TownCanvas.gameObject.SetActive(false);
            }
        }

        private void OnQuitHandler()
        {
            Game.TownManager.SceneData.Continue = false;

            SceneManager.LoadScene("Start");
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