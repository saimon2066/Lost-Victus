using System;
using Items;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        public CharacterController CharacterController;
        public PlayerInteraction PlayerInteraction;
        public PlayerMovement PlayerMovement;
        public CinemachineCamera PlayerCinemachine;
        public CinemachineBasicMultiChannelPerlin PlayerCinemachineNoise;
        public CinemachinePanTilt PlayerCinemachinePanTilt;
        public Transform HandTransform;

        [NonSerialized]
        public Item CurrentItem;
    }
}