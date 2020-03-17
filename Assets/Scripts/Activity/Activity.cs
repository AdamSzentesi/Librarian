using System;

namespace Librarian
{
    public abstract class Activity
    {
        protected bool IsFeelingInvolved { get; private set; } = false;
        private Feeling _FeelingInvolved;
        protected Feeling FeelingInvolved
        {
            get
            {
                return _FeelingInvolved;
            }
            set
            {
                _FeelingInvolved = value;
                IsFeelingInvolved = true;
            }
        }
        
        private Action _OnActivityEnd;
        public bool IsInProgress { get; protected set; } = false;

        public Activity()
        {
        }

        public Activity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public bool Begin(ActivityManager activityManager, Action onActivityEnd)
        {
            if (IsFeelingInvolved)
            {
                activityManager.ToggleFeelingBeingInvolved(FeelingInvolved, true);
            }

            _OnActivityEnd = onActivityEnd;

            return BeginInternal(activityManager);
        }

        public abstract bool BeginInternal(ActivityManager activityManager);

        public bool End(ActivityManager activityManager)
        {
            if (IsFeelingInvolved)
            {
                activityManager.ToggleFeelingBeingInvolved(FeelingInvolved, true);
            }

            if (_OnActivityEnd != null) _OnActivityEnd.Invoke();

            return EndInternal();
        }

        public abstract bool EndInternal();

    }
}