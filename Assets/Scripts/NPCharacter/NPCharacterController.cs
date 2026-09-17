using NPCharacter.Dialogue;
using Player;
using UnityEngine;

namespace NPCharacter
{
    public class NPCharacterController : MonoBehaviour, IInteractable
    {
        private enum Stage
        {
            NotMet, Waiting, Completed
        }
        
        [Header("Settings")]
        [SerializeField] private NPCharacterSO _npCharacterSO;

        private Stage _stage;
        
        public void Interact(PlayerController playerController)
        {
            bool isStarted = false;
            
            switch (_stage)
            {
                case Stage.NotMet:
                    _stage = Stage.Waiting;
                    isStarted = DialogueManager.Instance.StartDialogue(_npCharacterSO.DisplayName, _npCharacterSO.StartDialogue.Messages, Unfreeze);
                    break;
                case Stage.Waiting:
                    if (playerController.CurrentItem?.ItemSO == _npCharacterSO.RequiredItem)
                    {
                        _stage = Stage.Completed;
                        playerController.CurrentItem?.Destroy(playerController);
                        isStarted = DialogueManager.Instance.StartDialogue(_npCharacterSO.DisplayName, _npCharacterSO.ItemDialogue.Messages, Unfreeze);
                    }
                    else
                    {
                        isStarted = DialogueManager.Instance.StartDialogue(_npCharacterSO.DisplayName, _npCharacterSO.WrongItemDialogue.Messages, Unfreeze);
                    }
                    break;
                case Stage.Completed:
                    isStarted = DialogueManager.Instance.StartDialogue(_npCharacterSO.DisplayName, _npCharacterSO.AfterDialogue.Messages, Unfreeze);
                    break;
            }

            if (isStarted)
            {
                playerController.PlayerMovement.Freeze();
                playerController.PlayerInteraction.Freeze();
            }

            return;

            void Unfreeze()
            {
                playerController.PlayerMovement.Unfreeze();
                playerController.PlayerInteraction.Unfreeze();
            }
        }
    }
}