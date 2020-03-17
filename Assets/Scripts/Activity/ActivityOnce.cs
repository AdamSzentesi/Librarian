namespace Librarian
{
    public abstract class ActivityOnce : Activity
    {
        protected override void BeginInternal()
        {
            BeginImpl();
            Finish();
        }

        public abstract void BeginImpl();

        public override void Stop()
        {
        }

    }
}