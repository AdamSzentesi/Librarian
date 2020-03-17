using System;
using System.Collections.Generic;

namespace Librarian
{
    public class ReactionManager
    {
        private FeelingReaction[] _DefaultReactions = new FeelingReaction[Enum.GetValues(typeof(Feeling)).Length];
        private List<FeelingReaction> _ActiveReactions = new List<FeelingReaction>();
        private bool[] _ActiveReactionsStates = new bool[Enum.GetValues(typeof(Feeling)).Length];

        public ReactionManager()
        {
            _DefaultReactions[(int)Feeling.Fun] = new FeelingReaction(Feeling.Fun);
            _DefaultReactions[(int)Feeling.Calm] = new FeelingReaction(Feeling.Calm);
            _DefaultReactions[(int)Feeling.Fresh] = new FeelingReaction(Feeling.Fresh);
        }

        // Only react if reaction to a feeling is:
        // - not being processed
        // - being unproductive
        // - another feeling has urgent priority
        public void React(Feeling feeling)
        {
            FeelingReaction reaction = _DefaultReactions[(int)feeling];
            if (reaction == null || _ActiveReactionsStates[(int)feeling]) return;

            _ActiveReactions.Add(reaction);
            _ActiveReactionsStates[(int)feeling] = true;
            reaction.Activate();
        }

    }
}