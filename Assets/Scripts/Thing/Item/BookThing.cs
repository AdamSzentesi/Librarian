namespace Librarian
{
    public class BookThing : Pickupable
    {
        protected override void CreateItem(Item itemToSet)
        {
            itemToSet = new BookItem(0.333f);
        }
    }
}