using UnityEngine;

namespace Librarian
{
    public class InteractableSeed : BasicSeed
    {
        [Range(-100, 100)]
        public float FunBonus;
        [Range(-100, 100)]
        public float CalmBonus;
        [Range(-100, 100)]
        public float FreshBonus;

        public override InteractableItem CreateItem()
        {
            return new InteractableItem();
        }

    }
}