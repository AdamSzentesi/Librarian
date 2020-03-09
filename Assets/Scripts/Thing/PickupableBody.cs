using UnityEngine;

namespace Librarian
{
    [RequireComponent(typeof(PickupableSeed))]
    public class PickupableBody : InteractableBody
    {
        protected PickupableItem _PickupableItem { get { return _InteractableItem as PickupableItem; } }

        public void Init(PickupableItem item)
        {
            if (_IsInitialized) return;

            Debug.Log("PickupableItemBody: Init - external: " + gameObject);
            _InteractableItem = item;
            MainSprite = _PickupableItem.MainSprite;
            TopSprite = _PickupableItem.TopSprite;
            BottomSprite = _PickupableItem.BottomSprite;
            _IsInitialized = true;
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
