using System;

namespace Librarian
{
    public class PickUpActivity : Activity
    {
        private PickupableItem _Item;

        public PickUpActivity(PickupableItem item)
        {
            _Item = item;
        }

        public override bool BeginInternal(ActivityManager activityManager)
        {
            return activityManager.PickItem(_Item);
        }

        public override bool EndInternal()
        {
            throw new NotImplementedException();
        }
    }
}