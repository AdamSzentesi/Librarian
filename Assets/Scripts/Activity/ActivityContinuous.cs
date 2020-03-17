using System.Collections;
using UnityEngine;

namespace Librarian
{
    public abstract class ActivityContinuous : Activity
    {
        protected abstract void StartActivity();

        protected override sealed void StartInternal()
        {
            StartActivity();
        }

        protected abstract void ForceFinishActivity();

        public override sealed void ForceFinish()
        {
            ForceFinishActivity();
            Finish();
        }

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return OwnerActivityManager.StartCoroutine(coroutine);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            OwnerActivityManager.StopCoroutine(coroutine);
        }

    }
}