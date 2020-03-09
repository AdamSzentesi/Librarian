using UnityEngine;

namespace Librarian
{
    public class InteractableBody : BasicBody
    {
        [SerializeField]
        protected InteractableSeed _InteractableSeed;
        protected InteractableItem _InteractableItem;

        private int _RegisteredIndex = -1;

        protected override void Awake()
        {
            base.Awake();

            _RegisteredIndex = Level.RegisterInteractable(this);
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