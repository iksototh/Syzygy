using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine
{
    public class MainUI : MonoBehaviour
    {
        public Button StartBtn;
        public Button QuitBtn;

        private void Awake()
        {
            StartBtn.onClick.AddListener(StartClick);
            QuitBtn.onClick.AddListener(QuitClick);
        }

        private void StartClick()
        {
            SceneManager.LoadScene("World");
        }

        private void QuitClick()
        {
            Application.Quit();
        }
    }
}
