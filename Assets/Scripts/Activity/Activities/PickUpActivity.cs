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

        public override bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            return activityManager.PickItem(_Item);
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}