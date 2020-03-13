﻿using System;

namespace Librarian
{
    public class DeactivateTargetActivity : Activity
    {
        public FindTargetActivity _Activity;

        public DeactivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            return activityManager.DeactivateTarget(_Activity.Target);
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}