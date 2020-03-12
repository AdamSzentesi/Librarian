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

        public override bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            return activityManager.ActivateTarget(_Activity.Target);
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}
