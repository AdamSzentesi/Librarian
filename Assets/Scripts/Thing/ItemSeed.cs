using UnityEngine;

namespace Librarian
{
    public abstract class ItemSeed : ScriptableObject
    {
        public Sprite MainSprite;
        public Sprite TopSprite;
        public Sprite BottomSprite;

        public abstract Item CreateItem(PickupableItemBody body);
    }
}
