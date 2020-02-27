namespace Librarian
{
    public class Billboard : SceneItem
    {
        protected override void Start()
        {
            base.Start();

            transform.rotation = Level.CameraRotation;
            Init();
        }

        protected virtual void Init() { }

    }
}
