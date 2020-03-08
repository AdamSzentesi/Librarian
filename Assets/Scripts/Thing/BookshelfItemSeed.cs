using UnityEngine;

namespace Librarian
{
    public class BookshelfItemSeed : ItemSeed
    {
        public override Item CreateItem(PickupableItemBody body)
        {
            Debug.Log("BookshelfItemSeed: CreateItem for " + body.gameObject);

            Item item = new Item(body);

            //item.ma

            return item;
        }
    }
}
