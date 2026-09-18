using NPCharacter.Dialogue;
using Player;
using Quest;
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
        public NPCharacterSO NPCharacterSO;

        public bool IsMet
        {
            get { return _stage != Stage.NotMet; }
        }

        private Stage _stage;
        
        public void Interact(PlayerController playerController)
        {
            bool isStarted = false;

            if (NPCharacterSO.RequiredNPCs != null)
            {
                foreach (NPCharacterSO required in NPCharacterSO.RequiredNPCs)
                {
                    if (!QuestManager.Instance.IsCompleted(required))
                    {
                        isStarted = DialogueManager.Instance.StartDialogue(NPCharacterSO.DisplayName, NPCharacterSO.LockedDialogue.Messages, Unfreeze);
                        if (isStarted)
                        {
                            playerController.PlayerMovement.Freeze();
                            playerController.PlayerInteraction.Freeze();
                        }

                        return;
                    }
                }
            }
            
            switch (_stage)
            {
                case Stage.NotMet:
                    if (NPCharacterSO.RequiredItem == null)
                    {
                        _stage = Stage.Completed;
                    }
                    else
                    {
                        _stage = Stage.Waiting;
                        QuestManager.Instance.CheckQuests();
                    }
                    isStarted = DialogueManager.Instance.StartDialogue(NPCharacterSO.DisplayName, NPCharacterSO.StartDialogue.Messages, Unfreeze);
                    break;
                case Stage.Waiting:
                    if (playerController.CurrentItem != null && playerController.CurrentItem.ItemSO == NPCharacterSO.RequiredItem)
                    {
                        _stage = Stage.Completed;
                        playerController.CurrentItem?.Destroy(playerController);
                        isStarted = DialogueManager.Instance.StartDialogue(NPCharacterSO.DisplayName, NPCharacterSO.ItemDialogue.Messages, Unfreeze);
                    }
                    else
                    {
                        isStarted = DialogueManager.Instance.StartDialogue(NPCharacterSO.DisplayName, NPCharacterSO.WrongItemDialogue.Messages, Unfreeze);
                    }
                    break;
                case Stage.Completed:
                    isStarted = DialogueManager.Instance.StartDialogue(NPCharacterSO.DisplayName, NPCharacterSO.AfterDialogue.Messages, Unfreeze);
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
                
                if (_stage == Stage.Completed && !QuestManager.Instance.IsCompleted(NPCharacterSO))
                {
                    QuestManager.Instance.CompleteQuest(NPCharacterSO);
                }
            }
        }
    }
}