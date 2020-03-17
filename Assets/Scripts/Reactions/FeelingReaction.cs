namespace Librarian
{
    public class FeelingReaction : Reaction
    {
        public FeelingReaction(Feeling feeling)
        {
            FindTargetActivity findTarget = new FindTargetActivity(feeling);
            _Activities.Add(findTarget);
            _Activities.Add(new GoToTargetActivity(findTarget));
            _Activities.Add(new ActivateTargetActivity(findTarget));
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

    }
}

