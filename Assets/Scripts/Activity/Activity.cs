using System;

namespace Librarian
{
    public abstract class Activity
    {
        public bool IsInProgress { get; protected set; } = false;
        public abstract bool Begin(Character character, Action onActivityEnd);
        public abstract bool End();

    }
}