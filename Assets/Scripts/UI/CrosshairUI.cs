using System;
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

        private CrosshairState _currentState;

        public void SetCrosshair(CrosshairState state)
        {
            _currentState = state;
            
            switch (_currentState)
            {
                case CrosshairState.None:
                    _crosshair.enabled = false;
                    break;
                case CrosshairState.Grab:
                    _crosshair.enabled = true;
                    _crosshair.sprite = _crosshairGrab;
                    break;
                case CrosshairState.Tap:
                    _crosshair.enabled = true;
                    _crosshair.sprite = _crosshairTap;
                    break;
            }
        }
    }
}
