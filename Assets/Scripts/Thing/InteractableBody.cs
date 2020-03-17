using UnityEngine;

namespace Librarian
{
    public class InteractableBody : BasicBody
    {
        protected InteractableItem _InteractableItem;
        protected bool _IsInitialized = false;

        private int _RegisteredIndex = -1;

        protected override void Awake()
        {
            base.Awake();

            _RegisteredIndex = Level.RegisterInteractable(this);

            // TODO: this might be obsolete
            InteractableSeed seed = GetSeed();
            if (seed)
            {
                InteractableItem item = seed.CreateItem();
                Init(item);
            }
            else
            {
                Debug.LogWarning("InteractableBody: I have no Seed " + gameObject);
            }
        }

        public virtual void Init(InteractableItem item)
        {
            if (_IsInitialized || item == null) return;

            _InteractableItem = item;
        }

        protected virtual void Start()
        {
            _IsInitialized = true;
        }

        public virtual InteractableSeed GetSeed()
        {
            return GetComponent<InteractableSeed>();
        }
        
        protected virtual void OnDestroy()
        {
            Level.UnregisterInteractable(_RegisteredIndex);
        }

        public virtual bool Activate(ActivityManager activityManager, ActivityListInterface activityList)
        {
            Debug.Log("InteractableBody.Activate: " + _InteractableItem);
            return _InteractableItem.Activate(activityManager, activityList);
        }
        
        public virtual bool Deactivate(Character character)
        {
            return _InteractableItem.Deactivate(character);
        }

        public float GetBonus(Feeling feeling)
        {
            return _InteractableItem.GetBonus(feeling);
        }

    }
}