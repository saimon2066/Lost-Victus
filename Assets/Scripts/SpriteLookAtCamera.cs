    using UnityEngine;

    public class SpriteLookAtCamera: MonoBehaviour
    {
        [SerializeField] private bool _ignoreY;
           
        private Transform _cameraTransform;
        
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            Vector3 dir = _cameraTransform.forward;
            dir.y = _ignoreY ? 0f : dir.y;

            if (dir.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(dir);
            }
        }
    }
