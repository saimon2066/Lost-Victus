using System;
using Game.Input;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] 
        private float _speed;
        [SerializeField] 
        private float _walkFrequency;
        [SerializeField] 
        private float _stayFrequency;

        private PlayerController _playerController;

        private bool _isFrozen;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (_isFrozen)
            {
                return;
            }
            
            Vector2 moveAxis = InputManager.Instance.Inputs.Player.Move.ReadValue<Vector2>();
            
            _playerController.PlayerCinemachineNoise.FrequencyGain = moveAxis != Vector2.zero ? _walkFrequency : _stayFrequency;
            
            Vector3 motion = ((moveAxis.y * _playerController.CharacterController.transform.forward + moveAxis.x * _playerController.CharacterController.transform.right) * _speed + Physics.gravity) * Time.deltaTime;
            _playerController.CharacterController.Move(motion);
        }

        // cinemachine works in late update
        private void LateUpdate()
        {
            if (_isFrozen)
            {
                return;
            }
            
            Quaternion rot = Quaternion.Euler(0f, _playerController.PlayerCinemachine.transform.eulerAngles.y, 0f);
            _playerController.CharacterController.transform.rotation = rot;
        }

        public void Freeze()
        {
            _isFrozen = true;
            _playerController.PlayerCinemachinePanTilt.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Unfreeze()
        {
            _isFrozen = false;
            _playerController.PlayerCinemachinePanTilt.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}