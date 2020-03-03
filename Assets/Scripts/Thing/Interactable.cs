namespace Librarian
{
    public abstract class Interactable : Thing
    {
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

        public abstract bool Activate(Character character);
        
        public abstract bool Deactivate(Character character);

        public abstract float GetBonus(Feeling2 feeling);

    }
}