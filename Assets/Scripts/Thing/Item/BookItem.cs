namespace Librarian
{
    public class BookItem : Item2
    {
        public BookItem(Pickupable owner, string prefabPath, float funBonus) : base(owner, prefabPath)
        {
            _Bonuses[(int)Feeling2.Fun] = funBonus;
        }

        public override float GetBonus(Feeling2 feeling)
        {
            throw new System.NotImplementedException();
        }
    }
}