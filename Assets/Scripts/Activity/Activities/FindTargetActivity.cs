using System;

namespace Librarian
{
    public class FindTargetActivity : Activity
    {
        private Feeling _FeelingInvolved;
        public InteractableBody Target { get; private set; }

        public FindTargetActivity(Feeling feelingInvolved)
        {
            _FeelingInvolved = feelingInvolved;
        }

        public override bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            InteractableBody Target = Level.GetNearestInteractableBody(activityManager.Position, _FeelingInvolved);
            return Target;
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}