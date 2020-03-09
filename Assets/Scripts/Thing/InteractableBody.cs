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
        }

        protected virtual void Start()
        {
            if (!_IsInitialized)
            {
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

            //_InteractableItem.DebugMe();
        }

        public virtual InteractableSeed GetSeed()
        {
            return GetComponent<InteractableSeed>();
        }

        public virtual void Init(InteractableItem item)
        {
            if (_IsInitialized || item == null) return;

            _InteractableItem = item;
            _IsInitialized = true;
        }

        protected virtual void OnDestroy()
        {
            Level.UnregisterInteractable(_RegisteredIndex);
        }

        public virtual bool Activate(Character character)
        {
            return _InteractableItem.Activate(character);
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