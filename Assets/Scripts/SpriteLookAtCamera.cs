    using UnityEngine;

    [RequireComponent(typeof(Sprite))]
    public class SpriteLookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;
        
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            Vector3 dir = _cameraTransform.forward;
            dir.y = 0f;

            if (dir.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
