using System;

namespace Librarian
{
    public class InteractableItem
    {
        protected float[] _Bonuses;

        public InteractableItem()
        {
            _Bonuses = new float[Enum.GetNames(typeof(Feeling)).Length];
        }

        public bool Activate(Character character)
        {
            return true;
        }

        public bool Deactivate(Character character)
        {
            return false;
        }

        public float GetBonus(Feeling feeling)
        {
            return _Bonuses[(int)feeling];
        }

    }
}