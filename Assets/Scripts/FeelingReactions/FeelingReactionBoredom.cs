using UnityEngine;

namespace Librarian
{
    public class FeelingReactionBoredom : FeelingReaction
    {
        protected override bool InduceInternal(Character character)
        {
            if (!character) return false;

            Debug.Log("STARTED: FeelingReactionBoredom");

            Vector3 targetPosition = Level.GetNearestInteractablePosition(character.transform.position);

            return true;
        }

        public override void Stop()
        {
        }

    }
}