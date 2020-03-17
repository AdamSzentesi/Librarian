namespace Librarian
{
    public abstract class ActivityContinuous : Activity
    {
        protected void End()
        {
            Finish();
            EndInternal();
        }

        protected abstract void EndInternal();

    }
}