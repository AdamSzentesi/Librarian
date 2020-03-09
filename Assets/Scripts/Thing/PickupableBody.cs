using UnityEngine;

namespace Librarian
{
    public class PickupableBody : InteractableBody
    {
        protected PickupableItem _PickupableItem { get { return _InteractableItem as PickupableItem; } }

        public override InteractableSeed GetSeed()
        {
            return GetComponent<PickupableSeed>();
        }

        public void Init(PickupableItem item)
        {
            if (_IsInitialized || item == null) return;

            item.Body = this;
            MainSprite = item.MainSprite;
            TopSprite = item.TopSprite;
            BottomSprite = item.BottomSprite;

            base.Init(item);
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
