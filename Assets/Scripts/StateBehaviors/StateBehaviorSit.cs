using UnityEngine;

namespace Librarian
{
    [CreateAssetMenu(fileName = "SitAction", menuName = "Librarian/BehaviorAction/SitAction", order = 1)]
    public class StateBehaviorSit : StateBehavior
    {
        protected override bool InduceInternal()
        {
            Debug.Log("I am sitting.");
            return true;
        }

        public override void Stop()
        {
        }

    }
}