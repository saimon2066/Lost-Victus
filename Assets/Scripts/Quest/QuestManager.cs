using System;
using System.Collections.Generic;
using NPCharacter;
using TMPro;
using UnityEngine;

namespace Quest
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

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

        [SerializeField] private GameObject _questMarker;
        [SerializeField] private float _questMarkerHeight;
        [SerializeField] private TextMeshProUGUI _questDisplay;
        [SerializeField] private NPCharacterController[] _npcs;

        public Action QuestsCompleted;
        
        private readonly List<NPCharacterSO> _completedNPCs = new List<NPCharacterSO>();

        private void Start()
        {
            CheckQuests();
        }

        public void CheckQuests()
        {
            foreach (NPCharacterController npc in _npcs)
            {
                if (IsCompleted(npc.NPCharacterSO))
                {
                    continue;
                }

                _questDisplay.text = npc.IsMet ? npc.NPCharacterSO.ItemQuest : npc.NPCharacterSO.StartQuest;
                _questMarker.SetActive(true);
                _questMarker.transform.position = npc.transform.position + Vector3.up * _questMarkerHeight;
                
                return;
            }

            _questDisplay.text = "";
            _questMarker.SetActive(false);
            
            QuestsCompleted?.Invoke();
        }

        public void CompleteQuest(NPCharacterSO npCharacterSO)
        {
            if (_completedNPCs.Contains(npCharacterSO))
            {
                return;
            }
            
            _completedNPCs.Add(npCharacterSO);
            CheckQuests();
        }

        public bool IsCompleted(NPCharacterSO npCharacterSO)
        {
            return _completedNPCs.Contains(npCharacterSO);
        }
    }
}