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
        public GameObject QuestUIGameObject;
        public GameObject QuestDialogGameObject;
        public GameObject PauseMenuGameObject;
        public GameObject ConfirmMenuGameObject;

        public QuestUi QuestUi { get; private set; }
        public QuestDialog QuestDialog { get; private set; }
        public ConfirmMenu ConfirmMenu { get; private set; }

        private PauseMenu PauseMenu;

        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            QuestUi = QuestUIGameObject.GetComponent<QuestUi>();
            QuestDialog = QuestDialogGameObject.GetComponent<QuestDialog>();
            PauseMenu = PauseMenuGameObject.GetComponent<PauseMenu>();
            ConfirmMenu = ConfirmMenuGameObject.GetComponent<ConfirmMenu>();

            Instance = this;
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