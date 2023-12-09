using UnityEngine;
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
            if (!Game.Instance.GameData.IsPaused)
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
            if (Game.Instance.GameData.IsPaused)
            {
                gameObject.SetActive(false);
                Game.Instance.GameData.UnPause();
            }
            else
            {
                gameObject.SetActive(true);
                Game.Instance.GameData.Pause();
            }
        }

        private void OnSaveHandler()
        {
            Game.Instance.SavegameManager.Save();
        }

        private void OnLoadHandler()
        {
            Game.Instance.SavegameManager.Load();
        }
    }
}
