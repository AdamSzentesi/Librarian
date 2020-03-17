namespace Librarian
{
    public class ActivateTargetActivity : ActivityOnce
    {
        public FindTargetActivity _Activity;

        public ActivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override void StartActivity()
        {
            OwnerActivityManager.ActivateTarget(_Activity.Target, OwnerActivityList);
        }

    }
}
