using System;
using System.Collections.Generic;

namespace Librarian
{
    public class ReactionManager
    {
        private Reaction[] _DefaultReactions;

        private List<Reaction> _ActiveReactions = new List<Reaction>();
        private bool[] _ActiveReactionsStates = new bool[Enum.GetValues(typeof(Feeling)).Length];

        public ReactionManager(Reaction[] defaultReactions)
        {
            _DefaultReactions = defaultReactions;
        }

        public void React(Feeling feeling)
        {
            Reaction reaction = _DefaultReactions[(int)feeling];
            if (reaction == null || _ActiveReactionsStates[(int)feeling]) return;

            _ActiveReactions.Add(reaction);
            _ActiveReactionsStates[(int)feeling] = true;
            reaction.Activate();
        }

    }
}