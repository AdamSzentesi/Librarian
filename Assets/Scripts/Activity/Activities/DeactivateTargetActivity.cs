namespace Librarian
{
    public class DeactivateTargetActivity : ActivityOnce
    {
        public FindTargetActivity _Activity;

        public DeactivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override void BeginImpl()
        {
            ActivityManager.DeactivateTarget(_Activity.Target);
        }

    }
}
