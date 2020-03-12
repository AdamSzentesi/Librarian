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

        public override bool Begin(Character character, Action onActivityEnd)
        {
            return character.ActivateTarget(_Activity.Target);
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}
