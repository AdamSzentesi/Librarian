using System;

namespace Librarian
{
    public class FindTarget : OneShotActivity
    {
        public override bool Begin(Character character, Action onActivityEnd)
        {
            if (!character) return false;

            character.Target = Level.GetNearestInteractableBody(character.transform.position);
            return true;
        }

    }
}