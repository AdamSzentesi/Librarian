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

        public override bool Begin(Character character, Action onActivityEnd)
        {
            InteractableBody Target = Level.GetNearestInteractableBody(character.transform.position, _FeelingInvolved);
            return Target;
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}