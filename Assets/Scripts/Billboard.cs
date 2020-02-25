namespace Librarian
{
    public class Billboard : Item
    {
        private void Start()
        {
            transform.rotation = Level.CameraRotation;
            Init();
        }

        protected virtual void Init() { }

    }
}
