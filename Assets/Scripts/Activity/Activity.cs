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
        protected ActivityManager ActivityManager { get; private set; }

        public Activity()
        {
        }

        public Activity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public void Start(ActivityManager activityManager, Action onActivityEnd)
        {
            ActivityManager = activityManager;

            if (IsFeelingInvolved)
            {
                ActivityManager.ToggleFeelingBeingInvolved(FeelingInvolved, true);
            }

            _OnActivityEnd = onActivityEnd;

            BeginInternal();
            IsInProgress = true;
        }

        protected abstract void BeginInternal();

        public abstract void Stop();

        protected void Finish()
        {
            if (IsFeelingInvolved)
            {
                ActivityManager.ToggleFeelingBeingInvolved(FeelingInvolved, true);
            }

            if (_OnActivityEnd != null) _OnActivityEnd.Invoke();
            _OnActivityEnd = null;

            IsInProgress = false;
        }

    }
}