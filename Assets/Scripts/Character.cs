using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public enum State
    {
        Idle,
        Walk,
        Run,
        Sit,
    }

    public class Character : BasicBody
    {
        public PickupableBody DebugItem;
        public GameObject DebugItemBodyPrefab;

        public string Name;
        public TextMesh Stats;
        public float WalkSpeed = 1.0f;
        public float RunSpeed = 4.0f;
        public SpriteRenderer SpriteRenderer;

        private StateBehavior[] _StateBehaviors = new StateBehavior[Enum.GetNames(typeof(State)).Length];
        public StateBehavior IdleBehavior;
        public StateBehavior WalkBehavior;
        public StateBehavior RunBehavior;
        public StateBehavior SitBehavior;

        private FeelingReaction[] _FeelingReactions = new FeelingReaction[Enum.GetNames(typeof(Feeling)).Length];
        public FeelingReaction BoredomReaction;
        public FeelingReaction FearReaction;
        public FeelingReaction TirednessReaction;

        private bool _CanEvaluate = true;
        private Coroutine _EvaluateCooldownCoroutine;

        public Feelings StartingFeelings;

        [SerializeField]
        private FeelingManager _FeelingManager = new FeelingManager();
        private ActivityManager _ActivityManager;

        protected void Start()
        {
            _StateBehaviors[(int)State.Idle] = IdleBehavior;
            _StateBehaviors[(int)State.Walk] = WalkBehavior;
            _StateBehaviors[(int)State.Run] = RunBehavior;
            _StateBehaviors[(int)State.Sit] = SitBehavior;

            _FeelingReactions[(int)Feeling.Fun] = BoredomReaction;
            _FeelingReactions[(int)Feeling.Calm] = FearReaction;
            _FeelingReactions[(int)Feeling.Fresh] = TirednessReaction;

            _FeelingManager.Init(StartingFeelings);
            _ActivityManager = new ActivityManager(this);
        }

        private void Update()
        {
            _FeelingManager.Update();

            // TODO: feeling buffs

            EvaluateFeelings();

            _ActivityManager.Update();

            // DEBUG
            float fun = _FeelingManager.GetFeeling(Feeling.Fun);
            float calm = _FeelingManager.GetFeeling(Feeling.Calm);
            float fresh = _FeelingManager.GetFeeling(Feeling.Fresh);

            Stats.text = "FUN: " + fun + "\nCALM: " + calm + "\nFRESH: " + fresh;
        }

        private void EvaluateFeelings()
        {
            if (_CanEvaluate)
            {
                _CanEvaluate = false;

                Feeling result;
                if (_FeelingManager.EvaluateFeelings(out result))
                {
                    React(result);
                }

                _EvaluateCooldownCoroutine = StartCoroutine(EvaluateCooldown());
            }
        }

        private void React(Feeling feeling)
        {
            FeelingReaction feelingReaction = _FeelingReactions[(int)feeling];
            if (feelingReaction == null) return;

            feelingReaction.Induce(this);
        }

        public void InduceStateBehavior(State behavior)
        {
            StateBehavior behaviorAction = _StateBehaviors[(int)behavior];
            if (behaviorAction == null) return;

            behaviorAction.Induce();
        }

        private void OnDestroy()
        {
            if(_EvaluateCooldownCoroutine != null) StopCoroutine(_EvaluateCooldownCoroutine);
            _FeelingManager = null;
        }

        private IEnumerator EvaluateCooldown()
        {
            yield return new WaitForSeconds(2.0f);
            _CanEvaluate = true;
        }

        public bool AddActivity(Activity activity)
        {
            if (activity == null) return false;

            return _ActivityManager.AddActivity(activity);
        }

    }
}
