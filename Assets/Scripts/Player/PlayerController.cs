using System;
using Items;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("References")] 
        public PlayerInteraction PlayerInteraction;
        public PlayerMovement PlayerMovement;
        public CinemachineCamera PlayerCinemachine;
        public Transform HandTransform;

        [NonSerialized]
        public Item CurrentItem;
    }
}