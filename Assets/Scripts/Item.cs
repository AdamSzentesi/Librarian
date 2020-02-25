using UnityEngine;

namespace Librarian
{
    public class Item : MonoBehaviour
    {
        public virtual bool Activate() { return false; }
        public virtual bool Deactivate() { return true; }
    }
}
