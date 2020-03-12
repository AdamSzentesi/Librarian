using UnityEngine;

namespace Librarian
{
    public class FeelingReactionBoredom : FeelingReaction
    {
        protected override bool InduceInternal(Character character)
        {
            if (!character) return false;

            Debug.Log("STARTED: FeelingReactionBoredom");

            FindTargetActivity findTarget = new FindTargetActivity(Feeling.Fun);
            character.AddActivity(findTarget);
            character.AddActivity(new GoToTargetActivity(findTarget));
            character.AddActivity(new ActivateTargetActivity(findTarget));
            //character.AddActivity(new DeactivateTargetActivity(findTarget));

            return true;
        }

        public override void Stop()
        {
        }

    }
}