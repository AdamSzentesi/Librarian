using System;

namespace Librarian
{
    public class GoToTargetActivity : Activity
    {
        public InteractableBody _Target;

        /*
        public GoToTargetActivity(InteractableBody target)
        {
            _Target = target;
        }
        */

        public override bool Begin(Character character, Action onActivityEnd)
        {
            character.WalkTo(_Target);
            return true;
        }

        public override bool End()
        {
            throw new NotImplementedException();
        }
    }
}
