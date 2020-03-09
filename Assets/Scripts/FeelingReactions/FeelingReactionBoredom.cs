using UnityEngine;

namespace Librarian
{
    public class FeelingReactionBoredom : FeelingReaction
    {
        protected override bool InduceInternal(Character character)
        {
            if (!character) return false;

            Debug.Log("STARTED: FeelingReactionBoredom");

            InteractableBody target = Level.GetNearestInteractableBody(character.transform.position);
            character.WalkTo(target);

            //character.InduceStateBehavior(State.Walk);

            return true;
        }

        public override void Stop()
        {
        }

    }
}