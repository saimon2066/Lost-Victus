using Game.Input;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] 
        private float _speed;
        [SerializeField] 
        private float _walkFrequency;
        [SerializeField] 
        private float _stayFrequency;
        [Space]
        [Header("References")] 
        [SerializeField]
        private CinemachineCamera _playerCinemachine;
        [SerializeField]
        private CinemachineBasicMultiChannelPerlin _playerCinemachineNoise;
        [SerializeField] 
        private CharacterController _characterController;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Vector2 moveAxis = InputManager.Instance.Inputs.Player.Move.ReadValue<Vector2>();
            
            _playerCinemachineNoise.FrequencyGain = moveAxis != Vector2.zero ? _walkFrequency : _stayFrequency;
            
            Vector3 motion = ((moveAxis.y * _characterController.transform.forward + moveAxis.x * _characterController.transform.right) * _speed + Physics.gravity) * Time.deltaTime;
            _characterController.Move(motion);
        }

        // cinemachine works in late update
        private void LateUpdate()
        {
            Quaternion rot = Quaternion.Euler(0f, _playerCinemachine.transform.eulerAngles.y, 0f);
            _characterController.transform.rotation = rot;
        }
    }
}