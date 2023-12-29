using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandLine.Interaction
{
    public class InteractionManager : MonoBehaviour
    {
        private Mouse _mouse;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _mouse = Mouse.current;
        }

        void Update()
        {
            if (_mouse.rightButton.wasReleasedThisFrame)
            {
                var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_mouse.position.value), Vector2.up);
                if (hit.collider != null)
                {
                    Debug.Log($"HIT SOMETHING {hit.transform.name}");
                    var interactable = hit.collider.GetComponent<IInteractable>();
                    if(interactable != null) interactable.OnInteract();
                }
            }
        }
    }
}