using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class ActivityList
    {
        private Queue<Activity> _Activities = new Queue<Activity>();
        public bool IsRunning { get; private set; } = false;
        public CharacterInteface CharacterInteface { get; private set; }
        private int ActivityListIndex;

        public void AddActivity(Activity activity)
        {
            if (activity != null)
            {
                _Activities.Enqueue(activity);
            }
        }

        public void Start(CharacterInteface characterInteface, int activityListIndex)
        {
            if (IsRunning || characterInteface == null) return;

            Debug.Log("ActivityList.Start");

            CharacterInteface = characterInteface;
            ActivityListIndex = activityListIndex;
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
            nextActivity.Start(CharacterInteface, ActivityListIndex, OnActivityFinished);
        }

        // forced finish on whole queue
        public void Finish()
        {
            Debug.Log("ActivityList.Finish");

            IsRunning = false;
        }

        public void OnActivityFinished()
        {
            RunNextActivity();
        }

    }
}