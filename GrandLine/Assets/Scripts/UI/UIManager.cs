using GrandLine.UI.Dialogs;
using GrandLine.UI.Menus;
using GrandLine.UI.Player;
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

        public static QuestUi QuestUi;
        public static QuestDialog QuestDialog;
        public static ConfirmMenu ConfirmMenu;

        private static PauseMenu PauseMenu;

        private void Awake()
        {
            QuestUi = QuestUIGameObject.GetComponent<QuestUi>();
            QuestDialog = QuestDialogGameObject.GetComponent<QuestDialog>();
            PauseMenu = PauseMenuGameObject.GetComponent<PauseMenu>();
            ConfirmMenu = ConfirmMenuGameObject.GetComponent<ConfirmMenu>();
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