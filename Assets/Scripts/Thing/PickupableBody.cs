using UnityEngine;

namespace Librarian
{
    public class PickupableBody : InteractableBody
    {
        [SerializeField]
        protected PickupableSeed _PickupableSeed;

        protected PickupableItem _PickupableItem;
        protected override InteractableItem _InteractableItem { get { return _PickupableItem; } }

        private bool _IsInitialized = false;

        public void Init(PickupableItem item)
        {
            if (_IsInitialized) return;

            Debug.Log("PickupableItemBody: Init - external: " + gameObject);
            _PickupableItem = item;
            _IsInitialized = true;
        }

        private void Start()
        {
            if (!_IsInitialized)
            {
                if (_PickupableSeed)
                {
                    Debug.Log("PickupableItemBody: Start - from seed: " + gameObject);
                    _PickupableItem = _PickupableSeed.CreateItem(this);
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

            return character.PickItem(_PickupableItem);
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
