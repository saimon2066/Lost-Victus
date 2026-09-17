using System;
using Game.Input;
using NPCharacter;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private LayerMask _interactMask;
        [SerializeField] private float _interactDistance;
        
        [Header("Throw settings")] 
        public float ThrowPower;
        
        [Header("References")] 
        [SerializeField] private CrosshairUI _crosshairUI;
        
        private PlayerController _playerController;

        private bool _isFrozen;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

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
            if (_isFrozen)
            {
                _crosshairUI.SetCrosshair(CrosshairUI.CrosshairState.None);
                return;
            }
            
            Ray ray = new Ray(_playerController.PlayerCinemachine.transform.position, _playerController.PlayerCinemachine.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _interactDistance, _interactMask))
            {
                if (hit.collider.TryGetComponent(out IInteractable interactable))
                {
                    if (hit.collider.TryGetComponent(out NPCharacterController npCharacterController))
                    {
                        _crosshairUI.SetCrosshair(CrosshairUI.CrosshairState.Tap);
                    }
                    else
                    {
                        _crosshairUI.SetCrosshair(CrosshairUI.CrosshairState.Grab);
                    }

                    if (InputManager.Instance.Inputs.Player.Interact.WasPerformedThisFrame())
                    {
                        interactable.Interact(_playerController);
                    }
                }
            }
            else
            {
                _crosshairUI.SetCrosshair(CrosshairUI.CrosshairState.None);
            }
        }

        private void OnDropActionPerformed(InputAction.CallbackContext context)
        {
            if (_playerController.CurrentItem == null || _isFrozen)
            {
                return;
            }
            
            _playerController.CurrentItem.Drop(_playerController);
        }

        private void OnThrowActionPerformed(InputAction.CallbackContext context)
        {
            if (_playerController.CurrentItem == null || _isFrozen)
            {
                return;
            }

            _playerController.CurrentItem.Throw(_playerController);
        }

        public void Freeze()
        {
            _isFrozen = true;
        }

        public void Unfreeze()
        {
            _isFrozen = false;
        }
    }
}