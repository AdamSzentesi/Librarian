using System;

namespace Librarian
{
    public class FindTarget : OneShotActivity
    {
        public override bool Begin(Character character, Action onActivityEnd)
        {
            if (!character) return false;

            character.TargetPosition = Level.GetNearestInteractablePosition(character.transform.position);
            return true;
        }

    }
}