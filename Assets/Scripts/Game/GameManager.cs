using System;
using Player;
using Quest;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private RectTransform _winPanel;
        
        private void OnEnable()
        {
            QuestManager.Instance.QuestsCompleted += OnQuestsCompleted;
        }

        private void OnDisable()
        {
            QuestManager.Instance.QuestsCompleted -= OnQuestsCompleted;
        }

        private void OnQuestsCompleted()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _playerController.PlayerMovement.Freeze();
            _playerController.PlayerInteraction.Freeze();
            _winPanel.gameObject.SetActive(true);
        }
    }
}
