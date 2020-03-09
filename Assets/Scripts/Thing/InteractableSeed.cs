using UnityEngine;

namespace Librarian
{
    public class InteractableSeed : BasicSeed
    {
        public Feelings FeelingsBonus;

        public override InteractableItem CreateItem()
        {
            return new InteractableItem(this);
        }

    }
}