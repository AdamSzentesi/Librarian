using UnityEngine;

namespace Librarian
{
    [CreateAssetMenu(fileName = "BasicSeed", menuName = "Librarian/Seed/BasicSeed")]
    public abstract class BasicSeed : ScriptableObject
    {
        public string Name;

    }
}
