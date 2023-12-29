using GrandLine.Core.Models;
using GrandLine.UI.Dialogs;
using GrandLine.UI.Menus;
using GrandLine.UI.GameUi;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandLine.UI
{
    public class UIManager : MonoBehaviour
    {
        public Canvas QuestUICanvas;
        public Canvas QuestDialogCanvas;
        public Canvas PauseMenuCanvas;
        public Canvas ConfirmMenuCanvas;
        public Canvas ResourceUICanvas;

        public QuestUi QuestUi { get; private set; }
        public QuestDialog QuestDialog { get; private set; }
        public ConfirmMenu ConfirmMenu { get; private set; }

        private PauseMenu PauseMenu;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            QuestUICanvas.worldCamera = Camera.main;
            QuestDialogCanvas.worldCamera = Camera.main;
            PauseMenuCanvas.worldCamera = Camera.main;
            ConfirmMenuCanvas.worldCamera = Camera.main;
            ResourceUICanvas.worldCamera = Camera.main;

            QuestUi = QuestUICanvas.GetComponent<QuestUi>();
            QuestDialog = QuestDialogCanvas.GetComponent<QuestDialog>();
            PauseMenu = PauseMenuCanvas.GetComponent<PauseMenu>();
            ConfirmMenu = ConfirmMenuCanvas.GetComponent<ConfirmMenu>();

            QuestDialogCanvas.gameObject.SetActive(false);
            PauseMenuCanvas.gameObject.SetActive(false);
            ConfirmMenuCanvas.gameObject.SetActive(false);
            ResourceUICanvas.gameObject.SetActive(true);
            QuestUICanvas.gameObject.SetActive(true);

            Instance = this;
        }

        private void Start()
        {
            if(Game.Instance.GameData.IsPaused)
            {
                PauseMenuCanvas.gameObject.SetActive(true);
            }
        }

        public void LoadQuest(Quest quest)
        {
            QuestDialog.LoadQuest(quest);
        }

        public void ActivateQuest(Quest quest)
        {
            QuestUi.AddQuest(quest);
        }

        public void RemoveQuest(string questId)
        {
            QuestUi.RemoveQuest(questId);
        }

        void Update()
        {
            if (Keyboard.current?.escapeKey.wasReleasedThisFrame == true)
            {
                PauseMenu.OnPauseOrResumeHandler();
            }
        }
    }
}