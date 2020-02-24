using UnityEngine;

namespace Librarian
{
    public abstract class FeelingReaction : MonoBehaviour
    {
        public bool IsRunning { get; protected set; } = false;

        protected abstract bool InduceInternal(Character character);

        public abstract void Stop();

        public bool Induce(Character character)
        {
            if (IsRunning) return false;

            return InduceInternal(character);
        }
    }
}