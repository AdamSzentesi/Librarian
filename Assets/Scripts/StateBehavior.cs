using UnityEngine;

namespace Librarian
{
    public abstract class StateBehavior : ScriptableObject
    {
        public bool IsRunning { get; protected set; } = false;

        protected abstract bool InduceInternal();

        public abstract void Stop();

        public bool Induce()
        {
            if(IsRunning) return false;

            return InduceInternal();
        }

    }
}
