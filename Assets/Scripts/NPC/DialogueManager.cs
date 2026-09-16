using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPC
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        
        [Header("References")]
        [SerializeField] private RectTransform _dialoguePanel;
        [SerializeField] private TextMeshProUGUI _nameDisplay;
        [SerializeField] private TextMeshProUGUI _dialogueDisplay;
        [SerializeField] private Button _dialogueButton;

        private PlayerController _playerController;
        private NPCSO _npcSO;

        private int _dialogueIndex;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void StartDialogue(NPCSO npcSO, PlayerController playerController)
        {
            if ((_npcSO != null || _playerController != null) && (npcSO != null || playerController != null))
            {
                return;
            }

            _dialogueButton.onClick.AddListener(OnDialogueButtonClick);
            
            _npcSO = npcSO;
            _playerController = playerController;
            _playerController.PlayerMovement.Freeze();
            
            _dialoguePanel.gameObject.SetActive(true);
            _nameDisplay.text = _npcSO.DisplayName;
            _dialogueDisplay.text = _npcSO.Dialogues[_dialogueIndex];
            
            _dialogueIndex++;
        }
        
        private void OnDialogueButtonClick()
        {
            if (_dialogueIndex >= _npcSO.Dialogues.Length)
            {
                _dialogueButton.onClick.RemoveListener(OnDialogueButtonClick);
                
                _playerController.PlayerMovement.Unfreeze();
                _dialoguePanel.gameObject.SetActive(false);

                _dialogueIndex = 0;
                _playerController = null;
                _npcSO = null;
                
                return;
            }

            _dialogueDisplay.text = _npcSO.Dialogues[_dialogueIndex];
            _dialogueIndex++;
        }
    }
}