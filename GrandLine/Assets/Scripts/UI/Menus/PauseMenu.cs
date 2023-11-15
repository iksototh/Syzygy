using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine.UI.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        public Button ResumeBtn;
        public Button QuitBtn;
        public Button SaveBtn;
        public Button LoadBtn;

        public Canvas PauseMenuCanvas;

        void Start()
        {
            if (!Game.SceneData.IsPaused)
            {
                PauseMenuCanvas.gameObject.SetActive(false);
            }

            ResumeBtn.onClick.AddListener(OnPauseOrResumeHandler);
            QuitBtn.onClick.AddListener(OnQuitHandler);
            SaveBtn.onClick.AddListener(OnSaveHandler);
            LoadBtn.onClick.AddListener(OnLoadHandler);
        }

        private void OnQuitHandler()
        {
            SceneManager.LoadScene("Start");
        }

        private void OnPauseOrResumeHandler()
        {
            if (Game.SceneData.IsPaused)
            {
                PauseMenuCanvas.gameObject.SetActive(false);
                Game.SceneData.UnPause();
            }
            else
            {
                PauseMenuCanvas.gameObject.SetActive(true);
                Game.SceneData.Pause();
            }
        }

        private void OnSaveHandler()
        {
            Game.SavegameManager.Save();
        }

        private void OnLoadHandler()
        {
            Game.SavegameManager.Load();
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current?.escapeKey.wasReleasedThisFrame == true)
            {
                OnPauseOrResumeHandler();
            }
        }
    }
}
