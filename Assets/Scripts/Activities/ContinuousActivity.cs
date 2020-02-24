using System;

namespace Librarian
{
    public abstract class ContinuousActivity : Activity
    {
        private Action _OnActivityEnd;

        public override sealed bool Begin(Character character, Action onActivityEnd)
        {
            IsInProgress = BeginInternal(character);

            if (IsInProgress) _OnActivityEnd += onActivityEnd;

            return IsInProgress;
        }

        protected abstract bool BeginInternal(Character character);

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