﻿namespace Librarian
{
    public class PickUpActivity : ActivityOnce
    {
        private PickupableItem _Item;

        public PickUpActivity(PickupableItem item)
        {
            _Item = item;
        }

        public override void StartActivity()
        {
            OwnerCharacterInteface.PickItem(_Item);
        }

    }
}