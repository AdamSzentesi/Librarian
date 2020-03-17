using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class ActivityList
    {
        private Queue<Activity> _Activities = new Queue<Activity>();
        public bool IsRunning { get; private set; } = false;
        private ActivityManager _ActivityManager;

        public void AddActivity(Activity activity)
        {
            if (activity != null)
            {
                _Activities.Enqueue(activity);
            }
        }

        public void Start(ActivityManager activityManager)
        {
            if (IsRunning) return;
            if (activityManager == null) return;

            Debug.Log("ActivityList: Start");

            _ActivityManager = activityManager;
            IsRunning = true;
            RunNextActivity();
        }

        public void RunNextActivity()
        {
            if (_Activities.Count == 0)
            {
                Finish();
                return;
            }
            
            Activity nextActivity = _Activities.Dequeue();
            nextActivity.Start(_ActivityManager, OnActivityFinished);
        }

        // forced finish on whole queue
        public void Finish()
        {
            Debug.Log("ActivityList: Finish");

            IsRunning = false;
        }

        public void OnActivityFinished()
        {
            RunNextActivity();
        }

    }
}