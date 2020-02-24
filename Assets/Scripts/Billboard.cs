using UnityEngine;

namespace Librarian
{
    public class Billboard : MonoBehaviour
    {
        private void Start()
        {
            transform.rotation = Level.CameraRotation;
            Init();
        }

        protected virtual void Init() { }

    }
}
