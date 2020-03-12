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

        public override bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            activityManager.GoToTarget(_Activity.Target);
            return true;
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}
