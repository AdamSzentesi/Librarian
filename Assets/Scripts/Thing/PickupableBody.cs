using UnityEngine;

namespace Librarian
{
    public class PickupableBody : InteractableBody
    {
        [SerializeField]
        protected PickupableSeed _PickupableSeed;

        private bool _IsInitialized = false;

        public void Init(PickupableItem item)
        {
            if (_IsInitialized) return;

            Debug.Log("PickupableItemBody: Init - external: " + gameObject);
            _InteractableItem = item;
            SetupSprites(_InteractableItem);
            _IsInitialized = true;
        }

        private void Start()
        {
            if (!_IsInitialized)
            {
                if (_PickupableSeed)
                {
                    Debug.Log("PickupableItemBody: Start - from seed: " + gameObject);
                    _InteractableItem = _PickupableSeed.CreateItem(this);
                    SetupSprites(_InteractableItem);
                    _IsInitialized = true;
                }
                else
                {
                    Debug.LogWarning("PickupableItemBody: " + gameObject + " has no PickupableSeed!");
                }
            }
        }

        public sealed override bool Activate(Character character)
        {
            if (!character) return false;
            
            Deactivate(character);

            return character.PickItem(_InteractableItem);
        }

        public sealed override bool Deactivate(Character character)
        {
            return true;
        }

        public void Despawn()
        {
            Destroy(gameObject);
        }


    }
}
