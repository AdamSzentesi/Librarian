using System;

namespace Librarian
{
    public class FindTargetActivity : OneShotActivity
    {
        private Feeling _FeelingInvolved;

        public FindTargetActivity(Feeling feelingInvolved)
        {
            _FeelingInvolved = feelingInvolved;
        }

        public override bool Begin(Character character, Action onActivityEnd)
        {
            InteractableBody target = Level.GetNearestInteractableBody(character.transform.position, _FeelingInvolved);

            if (target)
            {
                character.WalkTo(target);
            }

            return target;
        }

    }
}