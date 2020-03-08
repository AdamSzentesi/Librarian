using UnityEngine;

namespace Librarian
{
    public class PickupableItemBody : InteractableItemBody
    {
        private Item _Item;
        private bool _IsInitialized = false;

        public void Init(Item item)
        {
            if (_IsInitialized) return;

            Debug.Log("PickupableItemBody: Init - external: " + gameObject);
            _Item = item;
            _IsInitialized = true;
        }

        private void Start()
        {
            if (!_IsInitialized)
            {
                if (_ItemSeed)
                {
                    Debug.Log("PickupableItemBody: Start - from seed: " + gameObject);
                    _Item = _ItemSeed.CreateItem(this);
                    _IsInitialized = true;
                }
                else
                {
                    Debug.LogWarning("PickupableItemBody: " + gameObject + " has no ItemSeed!");
                }
            }
        }

        public sealed override float GetBonus(Feeling feeling)
        {
            return _Item.GetBonus(feeling);
        }

        public sealed override bool Activate(Character character)
        {
            if (!character) return false;
            
            Deactivate(character);

            return character.PickItem(_Item);
        }

        public sealed override bool Deactivate(Character character)
        {
            return true;
        }

        public void Despawn()
        {
            Destroy(gameObject);
        }

    }
}
