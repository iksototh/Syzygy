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

        void Start()
        {
            if (!Game.SceneData.IsPaused)
            {
                gameObject.SetActive(false);
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

        public void OnPauseOrResumeHandler()
        {
            if (Game.SceneData.IsPaused)
            {
                gameObject.SetActive(false);
                Game.SceneData.UnPause();
            }
            else
            {
                gameObject.SetActive(true);
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
    }
}
