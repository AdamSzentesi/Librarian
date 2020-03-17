using System;

namespace Librarian
{
    public class DeactivateTargetActivity : Activity
    {
        public FindTargetActivity _Activity;

        public DeactivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override bool BeginInternal(ActivityManager activityManager)
        {
            return activityManager.DeactivateTarget(_Activity.Target);
        }

        public override bool EndInternal()
        {
            throw new NotImplementedException();
        }
    }
}
