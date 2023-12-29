using GrandLine.Interaction;
using GrandLine.Scenes;
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
            SceneLoader.LoadWorld();
        }
    }
}