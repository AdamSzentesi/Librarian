using UnityEngine;

namespace Librarian
{
    [CreateAssetMenu(fileName = "PickupableSeed", menuName = "Librarian/Seed/PickupableSeed")]
    public class PickupableSeed : BasicSeed
    {
        public Sprite Icon;

        public PickupableItem CreateItem(PickupableBody body)
        {
            return new PickupableItem(body);
        }

    }
}