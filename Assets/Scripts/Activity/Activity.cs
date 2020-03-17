using System;
using UnityEngine;

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
        protected ActivityManager OwnerActivityManager { get { return OwnerActivityList.ActivityManager; } }
        //protected ActivityList OwnerActivityList { get; private set; }
        protected ActivityListInterface OwnerActivityListInterface { get; private set; }

        public Activity() {}

        public Activity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public void Start(ActivityListInterface activityListInterface, Action onActivityEnd)
        {
            Debug.Log(" Activity.Start " + this);

            OwnerActivityListInterface = activityListInterface;
            _OnActivityEnd = onActivityEnd;

            StartInternal();
            IsInProgress = true;
        }

        protected abstract void StartInternal();

        public abstract void ForceFinish();

        protected void Finish()
        {
            Debug.Log(" Activity.Finish " + this);

            if (_OnActivityEnd != null)
            {
                _OnActivityEnd.Invoke();
                _OnActivityEnd = null;
            }

            IsInProgress = false;
        }

    }
}