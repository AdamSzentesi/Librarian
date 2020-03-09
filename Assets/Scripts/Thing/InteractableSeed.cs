using UnityEngine;

namespace Librarian
{
    [CreateAssetMenu(fileName = "InteractableSeed", menuName = "Librarian/Seed/InteractableSeed")]
    public class InteractableSeed : ScriptableObject
    {
        [Range(-100, 100)]
        public float FunBonus;
        [Range(-100, 100)]
        public float CalmBonus;
        [Range(-100, 100)]
        public float FreshBonus;

    }
}