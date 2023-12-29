using GrandLine.Interaction;
using GrandLine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Assets.Scripts.Towns
{
    public class Leave : MonoBehaviour, IInteractable
    {
        public void OnInteract()
        {
            UIManager.Instance.ConfirmMenu.EnterAction = OnEnter;
            UIManager.Instance.ConfirmMenu.Show();
        }

        void OnEnter() 
        {
            SceneManager.LoadScene("World");
        }
    }
}