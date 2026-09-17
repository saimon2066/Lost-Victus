using System;
using UnityEngine;

namespace Game.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        [NonSerialized]
        public IAAMain Inputs;

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

            if (Inputs == null)
            {
                Inputs = new IAAMain();
            }
        }

        private void OnEnable()
        {
            Inputs.Enable();
        }

        private void OnDisable()
        {
            Inputs.Disable();
        }
    }
}
