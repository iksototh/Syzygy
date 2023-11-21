using UnityEngine;
using UnityEngine.UI;

namespace GrandLine.UI
{
    public class Toggle : MonoBehaviour
    {
        public Button ToggleButton;
        public GameObject ToggleObject;
        
        void Awake()
        {
            ToggleButton.onClick.AddListener(OnToggleClick);
        }

        void OnToggleClick()
        {
            if(ToggleObject.activeSelf)
            {
                ToggleObject.SetActive(false);
            } else
            {
                ToggleObject.SetActive(true);
            }
        }
    }
}
