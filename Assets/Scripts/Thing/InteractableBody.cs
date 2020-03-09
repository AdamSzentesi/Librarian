using UnityEngine;

namespace Librarian
{
    [RequireComponent(typeof(InteractableSeed))]
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
                Debug.Log("InteractableBody: Init - start " + gameObject);
                _InteractableItem = GetComponent<InteractableSeed>().CreateItem();
                _IsInitialized = true;
            }
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