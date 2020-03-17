namespace Librarian
{
    public class ActivateTargetActivity : ActivityOnce
    {
        public FindTargetActivity _Activity;

        public ActivateTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        public override void BeginImpl()
        {
            ActivityManager.ActivateTarget(_Activity.Target);
        }

    }
}
