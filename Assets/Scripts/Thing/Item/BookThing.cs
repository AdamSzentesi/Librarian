namespace Librarian
{
    public class BookThing : Pickupable
    {
        protected override void CreateItem(ref Item itemToSet)
        {
            itemToSet = new BookItem(this, _PrefabPath, 0.333f);
        }
    }
}