﻿using System;

namespace Librarian
{
    public class GoToTargetActivity : Activity
    {
        public FindTargetActivity _Activity;

        public GoToTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override bool Begin(Character character, Action onActivityEnd)
        {
            character.GoToTarget(_Activity.Target);
            return true;
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}
