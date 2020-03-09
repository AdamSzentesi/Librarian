using UnityEngine;

namespace Librarian
{
    public class PickupableSeed : InteractableSeed
    {
        public Sprite Icon;

        public override InteractableItem CreateItem()
        {
            PickupableBody body = GetComponent<PickupableBody>();
            if (!body) return null;

            return new PickupableItem(this, body);
        }

    }
}