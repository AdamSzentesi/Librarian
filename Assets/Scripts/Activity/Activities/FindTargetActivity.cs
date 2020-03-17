using System;

namespace Librarian
{
    public class FindTargetActivity : Activity
    {
        public InteractableBody Target { get; private set; }

        public FindTargetActivity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public override bool BeginInternal(ActivityManager activityManager)
        {
            InteractableBody Target = Level.GetNearestInteractableBody(activityManager.Position, FeelingInvolved);
            return Target;
        }

        public override bool EndInternal()
        {
            throw new NotImplementedException();
        }
    }
}