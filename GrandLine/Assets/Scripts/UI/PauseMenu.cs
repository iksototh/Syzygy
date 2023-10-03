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
        public Canvas PauseMenuCanvas;

        bool IsPaused = false;

        private void Awake()
        {
            PauseMenuCanvas.gameObject.SetActive(false);
        }

        void Start()
        {
            ResumeBtn.onClick.AddListener(OnPauseOrResume);
            QuitBtn.onClick.AddListener(OnQuit);
        }

        private void OnQuit()
        {
            SceneManager.LoadScene("Bootstrap");
        }

        private void OnPauseOrResume()
        {
            Debug.Log($"Pause and resume {Game.IsPaused}");
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

        // Update is called once per frame
        void Update()
        {
            if(Keyboard.current?.escapeKey.wasReleasedThisFrame == true)
            {
                OnPauseOrResume();
            }
        }
    }
}
