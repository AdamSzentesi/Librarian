using UnityEngine;

namespace Librarian
{
    public class Obstacle : Billboard
    {
        public Collider Collider;

        private void Awake()
        {
            if (!Collider)
            {
                Debug.LogError("Obstacle: I have no collider!");
            }
        }


    }
}
