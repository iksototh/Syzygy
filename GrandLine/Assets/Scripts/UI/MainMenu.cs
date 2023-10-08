using GrandLine.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrandLine.UI
{
    public class MainUI : MonoBehaviour
    {
        public Button StartBtn;
        public Button QuitBtn;
        public Button ContinueBtn;

        public SceneData SceneData;

        private void Awake()
        {
            StartBtn.onClick.AddListener(OnStartClick);
            QuitBtn.onClick.AddListener(OnQuitClick);
            ContinueBtn.onClick.AddListener(OnContinueClick);
        }

        private void OnContinueClick()
        {
            var file = Path.Combine(Application.persistentDataPath, "savefile.save");
            
            if(File.Exists(file))
            {
                SceneData.Continue = true;
                SceneData.Pause();
            }
            SceneManager.LoadScene("World");
        }

        private void OnStartClick()
        {
            SceneData.Continue = false;
            SceneData.UnPause();
            SceneManager.LoadScene("World");
        }

        private void OnQuitClick()
        {
            Application.Quit();
        }
    }
}
