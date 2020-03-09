using UnityEngine;

namespace Librarian
{
    public class BasicItem
    {
        public Sprite MainSprite { get; private set; }
        public Sprite TopSprite { get; private set; }
        public Sprite BottomSprite { get; private set; }

        public BasicItem(PickupableBody body)
        {
            if (body)
            {
                MainSprite = body.MainSprite;
                TopSprite = body.TopSprite;
                BottomSprite = body.BottomSprite;
            }
        }

    }
}