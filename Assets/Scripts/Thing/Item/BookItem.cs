using UnityEngine;

namespace Librarian
{
    public class BookItem : Item
    {
        public BookItem(float funBonus) : this(null, string.Empty, funBonus)
        {
        }

        public BookItem(Pickupable owner, string prefabPath, float funBonus) : base(owner, prefabPath)
        {
            Debug.Log("path0 " + prefabPath);
            _Bonuses[(int)Feeling.Fun] = funBonus;
        }

        public override float GetBonus(Feeling feeling)
        {
            throw new System.NotImplementedException();
        }
    }
}