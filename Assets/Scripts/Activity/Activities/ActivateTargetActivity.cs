using System;

namespace Librarian
{
    public class ActivateTargetActivity : Activity
    {
        public FindTargetActivity _Activity;

        public ActivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override bool BeginInternal(ActivityManager activityManager)
        {
            return activityManager.ActivateTarget(_Activity.Target);
        }

        public override bool EndInternal()
        {
            throw new NotImplementedException();
        }
    }
}
