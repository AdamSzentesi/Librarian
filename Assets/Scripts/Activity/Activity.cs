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
        protected CharacterInteface OwnerCharacterInteface { get; private set; }
        protected int ActivityListIndex { get; private set; }

        public Activity() {}

        public Activity(Feeling feelingInvolved)
        {
            FeelingInvolved = feelingInvolved;
        }

        public void Start(CharacterInteface characterInteface, int activityListIndex, Action onActivityEnd)
        {
            Debug.Log("+ Activity.Start " + this);

            OwnerCharacterInteface = characterInteface;
            _OnActivityEnd = onActivityEnd;

            StartInternal();
            IsInProgress = true;
        }

        protected abstract void StartInternal();

        public abstract void ForceFinish();

        protected void Finish()
        {
            Debug.Log("- Activity.Finish " + this);

            if (_OnActivityEnd != null)
            {
                _OnActivityEnd.Invoke();
                _OnActivityEnd = null;
            }

            IsInProgress = false;
        }

    }
}