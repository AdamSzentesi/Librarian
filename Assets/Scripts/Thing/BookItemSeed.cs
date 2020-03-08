using UnityEngine;

namespace Librarian
{
    [CreateAssetMenu(fileName = "BookItemSeed", menuName = "Librarian/ItemSeed/BookItemSeed")]
    public class BookItemSeed : ItemSeed
    {
        public override Item CreateItem(PickupableItemBody body)
        {
            Debug.Log("BookItemSeed: CreateItem for " + body.gameObject);

            return new Item(body);
        }
    }
}