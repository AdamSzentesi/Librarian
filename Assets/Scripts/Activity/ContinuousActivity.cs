using System;

namespace Librarian
{
    public abstract class ContinuousActivity : Activity
    {
        private Action _OnActivityEnd;

        public override sealed bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            IsInProgress = BeginInternal(activityManager);

            if (IsInProgress) _OnActivityEnd += onActivityEnd;

            return IsInProgress;
        }

        protected abstract bool BeginInternal(ActivityManager activityManager);

        public override sealed bool End()
        {
            IsInProgress = !EndInternal();

            if (!IsInProgress && _OnActivityEnd != null) _OnActivityEnd.Invoke();

            return !IsInProgress;
        }

        protected abstract bool EndInternal();

        ~ContinuousActivity()
        {
            _OnActivityEnd = null;
        }

    }
}