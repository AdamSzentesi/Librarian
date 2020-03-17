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

        public sealed override bool Activate(ActivityManager activityManager, ActivityListInterface activityList)
        {
            Debug.Log("+++ ACT " + this + " " + activityManager);
            if (activityManager == null) return false;
            
            //Deactivate(activityManager);

            activityList.AddActivity(new PickUpActivity(_PickupableItem));

            //activityManager.add
            //return character.add(activity);

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
