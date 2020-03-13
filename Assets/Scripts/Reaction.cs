using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public abstract class Reaction
    {
        private List<Activity> _Activities = new List<Activity>();

        public abstract void Activate();

        public bool AddActivity(Activity activity)
        {
            Debug.Log("ADDING ACTIVITY: " + activity);
            _Activities.Add(activity);
            return true;
        }

    }
}
