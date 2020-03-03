using UnityEngine;

namespace Librarian
{
    public class Thing : MonoBehaviour
    {
        public bool IsBillboard = true;
        public Transform BillboardParent;
        public Collider Trigger;
        public Collider Obstacle;

        protected virtual void Awake()
        {
            if (IsBillboard)
            {
                if (!BillboardParent)
                {
                    BillboardParent = transform;
                }

                BillboardParent.rotation = Level.CameraRotation;
            }
        }

    }
}