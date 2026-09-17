using Player;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IInteractable
    {
        public ItemSO ItemSO;
        
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Interact(PlayerController playerController)
        {
            if (playerController.CurrentItem != null)
            {
                return;
            }
            
            playerController.CurrentItem = this;

            _rigidbody.isKinematic = true;
            _rigidbody.interpolation = RigidbodyInterpolation.None;
            transform.parent = playerController.HandTransform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }

        public void Drop(PlayerController playerController)
        {
            playerController.CurrentItem = null;

            transform.parent = null;
            transform.gameObject.layer = LayerMask.NameToLayer("Interactable");
            _rigidbody.isKinematic = false;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        public void Throw(PlayerController playerController)
        {
            Drop(playerController);
            _rigidbody.AddForce(playerController.PlayerCinemachine.transform.forward.normalized * playerController.PlayerInteraction.ThrowPower, ForceMode.Impulse);
        }

        public void Destroy(PlayerController playerController)
        {
            playerController.CurrentItem = null;
            Destroy(gameObject);
        }
    }
}