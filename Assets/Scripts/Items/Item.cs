using Player;
using UnityEngine;

namespace Items
{
    [RequireComponent(typeof(Rigidbody))]
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] 
        private ItemSO _itemSO;
        
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
        }

        public void Throw(PlayerController playerController)
        {
            playerController.CurrentItem = null;
            
            transform.parent = null;
            transform.gameObject.layer = LayerMask.NameToLayer("Interactable");
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(playerController.PlayerCinemachine.transform.forward.normalized * playerController.PlayerInteraction.ThrowPower, ForceMode.Impulse);
        }
    }
}