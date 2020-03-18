using UnityEngine;

namespace Librarian
{
    public class PickupableBody : InteractableBody
    {
        protected PickupableItem _PickupableItem { get { return _InteractableItem as PickupableItem; } }

        public override InteractableSeed GetSeed()
        {
            return GetComponent<PickupableSeed>();
        }

        public void Init(PickupableItem item)
        {
            if (_IsInitialized || item == null) return;

            item.Body = this;
            MainSprite = item.MainSprite;
            TopSprite = item.TopSprite;
            BottomSprite = item.BottomSprite;

            base.Init(item);
        }

        public sealed override bool Activate(CharacterInteface characterInterface, int activityListIndex)
        {
            if (characterInterface == null) return false;
            
            //Deactivate(activityManager);

            characterInterface.AddActivity(new PickUpActivity(_PickupableItem), activityListIndex);

            return true;
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
