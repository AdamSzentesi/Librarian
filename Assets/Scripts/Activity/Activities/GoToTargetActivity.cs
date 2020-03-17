using System;

namespace Librarian
{
    public class GoToTargetActivity : Activity
    {
        public FindTargetActivity _Activity;

        public GoToTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override bool BeginInternal(ActivityManager activityManager)
        {
            activityManager.GoToTarget(_Activity.Target);
            return true;
        }

        public override bool EndInternal()
        {
            throw new NotImplementedException();
        }
    }
}
