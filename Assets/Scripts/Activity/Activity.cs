using System;

namespace Librarian
{
    public abstract class Activity
    {
        public bool IsInProgress { get; protected set; } = false;
        public abstract bool Begin(ActivityManager activityManager, Action onActivityEnd);
        public abstract bool End();

    }
}