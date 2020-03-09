using UnityEngine;

namespace Librarian
{
    [DisallowMultipleComponent]
    public abstract class BasicSeed : MonoBehaviour
    {
        public abstract InteractableItem CreateItem();

    }
}