using System;
using Game.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace NPCharacter.Dialogue
{
    [Serializable]
    public struct Dialogue
    {
        [TextArea] public string[] Messages;
    }
    
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        
        [Header("References")]
        [SerializeField] private RectTransform _dialoguePanel;
        [SerializeField] private TextMeshProUGUI _nameDisplay;
        [SerializeField] private TextMeshProUGUI _dialogueDisplay;

        private Action _dialogueFinished;
        
        private string[] _dialogue;
        private int _dialogueIndex;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                Debug.LogWarning("Two or more singletons in the same scene!");
            }
        }

        private void OnEnable()
        {
            InputManager.Instance.Inputs.Dialogue.Continue.performed += OnContinueActionPerformed;
        }

        private void OnDisable()
        {
            InputManager.Instance.Inputs.Dialogue.Continue.performed -= OnContinueActionPerformed;
        }

        public bool StartDialogue(string displayName, string[] dialogue, Action dialogueFinished = null)
        {
            if (dialogue == null || dialogue.Length == 0 || _dialogue != null)
            {
                return false;
            }
            
            _dialogueIndex = 0;
            _dialogue = dialogue;
            
            _dialogueFinished = dialogueFinished;
            _dialoguePanel.gameObject.SetActive(true);
            _nameDisplay.text = displayName;
            _dialogueDisplay.text = _dialogue[_dialogueIndex];
            _dialogueIndex++;

            return true;
        }
        
        private void StopDialogue()
        {
            _dialoguePanel.gameObject.SetActive(false);
            _dialogueIndex = 0;
            _dialogue = null;
            
            _dialogueFinished?.Invoke();
            _dialogueFinished = null;
        }

        private void OnContinueActionPerformed(InputAction.CallbackContext context)
        {
            if (_dialogue == null)
            {
                return;
            }
            if (_dialogueIndex >= _dialogue.Length)
            {
                StopDialogue();
                return;
            }

            _dialogueDisplay.text = _dialogue[_dialogueIndex];
            _dialogueIndex++;
        }
    }
}