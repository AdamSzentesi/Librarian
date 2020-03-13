namespace Librarian
{
    public class NoFunReaction : Reaction
    {

        public NoFunReaction()
        {
            FindTargetActivity findTarget = new FindTargetActivity(Feeling.Fun);
            AddActivity(findTarget);
            AddActivity(new GoToTargetActivity(findTarget));
            AddActivity(new ActivateTargetActivity(findTarget));
            //AddActivity(new DeactivateTargetActivity(findTarget));
        }

        public override void Activate()
        {
            throw new System.NotImplementedException();
        }

    }
}

