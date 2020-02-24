using UnityEngine;

namespace Librarian
{
    public abstract class StateBehavior : ScriptableObject
    {
        public bool IsRunning { get; protected set; } = false;

        protected abstract bool StartInternal();

        public abstract void Stop();

        public bool Start()
        {
            if(IsRunning) return false;

            return StartInternal();
        }

    }
}
