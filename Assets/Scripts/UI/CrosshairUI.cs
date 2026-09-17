using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CrosshairUI : MonoBehaviour
    {
        public enum CrosshairState
        {
            None, Grab, Tap
        }

        [SerializeField] private Sprite _crosshairGrab;
        [SerializeField] private Sprite _crosshairTap;
        [SerializeField] private Image _crosshair;
        public TextMeshProUGUI ItemDisplay;

        private CrosshairState _currentState;

        public void SetCrosshair(CrosshairState state)
        {
            _currentState = state;
            
            switch (_currentState)
            {
                case CrosshairState.None:
                    _crosshair.gameObject.SetActive(false);
                    break;
                case CrosshairState.Grab:
                    _crosshair.gameObject.SetActive(true);
                    _crosshair.sprite = _crosshairGrab;
                    break;
                case CrosshairState.Tap:
                    _crosshair.gameObject.SetActive(true);
                    _crosshair.sprite = _crosshairTap;
                    break;
            }
        }
    }
}
