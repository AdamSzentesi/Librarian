using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public abstract class Reaction
    {
        protected List<Activity> _Activities = new List<Activity>();

        public abstract void Activate();
    }
}
