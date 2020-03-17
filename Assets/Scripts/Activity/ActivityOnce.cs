namespace Librarian
{
    public abstract class ActivityOnce : Activity
    {
        protected override sealed void StartInternal()
        {
            StartActivity();
            Finish();
        }

        public abstract void StartActivity();

        public override sealed void ForceFinish() {}

    }
}