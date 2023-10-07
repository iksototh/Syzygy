using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine
{
    public class PauseMenu : MonoBehaviour
    {
        public Button ResumeBtn;
        public Button QuitBtn;
        public Button SaveBtn;
        public Button LoadBtn;

        public Canvas PauseMenuCanvas;

        bool IsPaused = false;

        private void Awake()
        {
            PauseMenuCanvas.gameObject.SetActive(false);
        }

        void Start()
        {
            ResumeBtn.onClick.AddListener(OnPauseOrResumeHandler);
            QuitBtn.onClick.AddListener(OnQuitHandler);
            SaveBtn.onClick.AddListener(OnSaveHandler);
            LoadBtn.onClick.AddListener(OnLoadHandler); 
        }

        private void OnQuitHandler()
        {
            SceneManager.LoadScene("Bootstrap");
        }

        private void OnPauseOrResumeHandler()
        {
            if(IsPaused)
            {
                PauseMenuCanvas.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                IsPaused = false;
            }
            else
            {
                PauseMenuCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
                IsPaused = true;
            }
        }

        private void OnSaveHandler()
        {
            Game.SavegameManager.OnSave();
        }

        private void OnLoadHandler()
        {
            Game.SavegameManager.OnLoad();
        }

        // Update is called once per frame
        void Update()
        {
            if(Keyboard.current?.escapeKey.wasReleasedThisFrame == true)
            {
                OnPauseOrResumeHandler();
            }
        }
    }
}
