using System;
using UnityEngine;

namespace Librarian
{
    public class InteractableItem
    {
        protected float[] _Bonuses;

        public InteractableItem(InteractableSeed seed)
        {
            _Bonuses = new float[Enum.GetNames(typeof(Feeling)).Length];
            _Bonuses[(int)Feeling.Fun] = seed.FeelingsBonus.Fun;
            _Bonuses[(int)Feeling.Calm] = seed.FeelingsBonus.Calm;
            _Bonuses[(int)Feeling.Fresh] = seed.FeelingsBonus.Fresh;
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

        public virtual void DebugMe()
        {
            Debug.Log("Debug item:");
            foreach (Feeling feeling in Enum.GetValues(typeof(Feeling)))
            {
                Debug.Log(" - " + feeling + ": " + _Bonuses[(int)feeling]);
            }
        }

    }
}