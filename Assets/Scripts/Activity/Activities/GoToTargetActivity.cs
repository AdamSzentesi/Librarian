namespace Librarian
{
    public class GoToTargetActivity : ActivityContinuous
    {
        private FindTargetActivity _Activity;

        public GoToTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        protected override void StartActivity()
        {
            OwnerActivityManager.GoToTarget(_Activity.Target, OnTargetReached);
        }

        protected override void ForceFinishActivity()
        {
        }

        private void OnTargetReached()
        {
            Finish();
        }

    }
}
