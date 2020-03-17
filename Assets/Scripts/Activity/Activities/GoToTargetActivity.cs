using System;
using System.Collections;
using UnityEngine;

namespace Librarian
{
    public class GoToTargetActivity : ActivityContinuous
    {
        public FindTargetActivity _Activity;

        private Coroutine DebugCor;

        public GoToTargetActivity(FindTargetActivity activity)
        {
            _Activity = activity;
        }

        protected override void BeginInternal()
        {
            ActivityManager.GoToTarget(_Activity.Target);
            DebugCor = ActivityManager.StartCoroutine(cor());
        }

        public override void Stop()
        {
            //if(DebugCor != null) activityManager.StopCoroutine(DebugCor);
        }

        protected override void EndInternal()
        {
            throw new NotImplementedException();
        }

        private IEnumerator cor()
        {
            yield return new WaitForSecondsRealtime(5.0f);
            End();
        }
    }
}
