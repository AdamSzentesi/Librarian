namespace Librarian
{
    public abstract class OneShotActivity : Activity
    {
        public override sealed bool End()
        {
            return true;
        }

    }
}