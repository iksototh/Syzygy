using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI.Menus
{
    public class ConfirmMenu : MonoBehaviour
    {
        public Button EnterBtn;
        public Button LeaveBtn;

        public Canvas ConfirmCanvas;

        void Awake()
        {
            //EnterBtn.onClick.AddListener(Enter);
            LeaveBtn.onClick.AddListener(Leave);
        }

        private void Start()
        {
            if (!Game.SceneData.IsPaused)
            {
                ConfirmCanvas.gameObject.SetActive(false);
            }

            // Game.SceneData.Pause();
        }

        public void Show()
        {
            Game.SceneData.Pause();
            ConfirmCanvas.gameObject.SetActive(true);
        }

        private void Enter()
        {
            Debug.Log("Enter");

        }

        private void Leave()
        {
            Game.SceneData.UnPause();
            ConfirmCanvas.gameObject.SetActive(false);
        }
    }
}
