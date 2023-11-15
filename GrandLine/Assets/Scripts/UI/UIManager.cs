using GrandLine.UI.Player;
using UnityEngine;

namespace GrandLine.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject QuestUIGameObject;
        public static QuestUi QuestUi;

        private void Awake()
        {
            QuestUi = QuestUIGameObject.GetComponent<QuestUi>();
        }
    }
}