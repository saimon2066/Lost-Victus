using System;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] 
        private LayerMask _interactMask;
        [SerializeField]
        private float _interactDistance;
        [Space]
        [Header("Throw settings")] 
        public float ThrowPower;
        [Space]
        [Header("References")] 
        [SerializeField]
        private PlayerController _playerController;

        private void OnEnable()
        {
            InputManager.Instance.Inputs.Player.Drop.performed += OnDropActionPerformed;
            InputManager.Instance.Inputs.Player.Throw.performed += OnThrowActionPerformed;
        }

        private void OnDisable()
        {
            InputManager.Instance.Inputs.Player.Drop.performed -= OnDropActionPerformed;
            InputManager.Instance.Inputs.Player.Throw.performed -= OnThrowActionPerformed;
        }

        private void Update()
        {
            if (!InputManager.Instance.Inputs.Player.Interact.WasPerformedThisFrame())
            {
                return;
            }
            
            Ray ray = new Ray(_playerController.PlayerCinemachine.transform.position, _playerController.PlayerCinemachine.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance, _interactMask))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(_playerController);
                }
            }
        }

        private void OnDropActionPerformed(InputAction.CallbackContext context)
        {
            if (_playerController.CurrentItem == null)
            {
                return;
            }
            
            _playerController.CurrentItem.Drop(_playerController);
        }

        private void OnThrowActionPerformed(InputAction.CallbackContext context)
        {
            if (_playerController.CurrentItem == null)
            {
                return;
            }

            _playerController.CurrentItem.Throw(_playerController);
        }
    }
}