using GrandLine.UI;
using System.Collections;
using UnityEngine;

namespace GrandLine.Assets.Scripts.Managers
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