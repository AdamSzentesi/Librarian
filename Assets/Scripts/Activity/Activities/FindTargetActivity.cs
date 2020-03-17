using System;

namespace Librarian
{
    public class FindTargetActivity : ActivityOnce
    {
        public InteractableBody Target { get; private set; }

        public FindTargetActivity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public override void StartActivity()
        {
            Target = Level.GetNearestInteractableBody(OwnerActivityManager.Position, FeelingInvolved);
        }

    }
}