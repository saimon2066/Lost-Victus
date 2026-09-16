using Player;
using UnityEngine;

namespace NPC
{
    public class NPCController : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        [SerializeField] private NPCSO _npcSO;
        
        public void Interact(PlayerController playerController)
        {
           DialogueManager.Instance.StartDialogue(_npcSO, playerController);
        }
    }
}