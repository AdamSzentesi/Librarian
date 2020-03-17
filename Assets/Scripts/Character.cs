using System;
using System.Collections;
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

        private bool _CanEvaluate = true;
        private Coroutine _EvaluateCooldownCoroutine;

        public Feelings StartingFeelings;

        [SerializeField]
        private FeelingManager _FeelingManager;
        private ReactionManager _ReactionManager;
        private ActivityManager _ActivityManager;

        protected void Start()
        {
            _StateBehaviors[(int)State.Idle] = IdleBehavior;
            _StateBehaviors[(int)State.Walk] = WalkBehavior;
            _StateBehaviors[(int)State.Run] = RunBehavior;
            _StateBehaviors[(int)State.Sit] = SitBehavior;

            // SETUP FEELINGS
            _FeelingManager = new FeelingManager(StartingFeelings);

            // SETUP REACTIONS
            _ReactionManager = new ReactionManager();

            // SETUP ACTIVITIES
            _ActivityManager = new ActivityManager(this);

            // DEBUG
            ActivityList debugActivityList = new ActivityList();
            FindTargetActivity find = new FindTargetActivity(Feeling.Fun);
            debugActivityList.AddActivity(find);
            debugActivityList.AddActivity(new GoToTargetActivity(find));
            debugActivityList.AddActivity(new ActivateTargetActivity(find));
            _ActivityManager.AddActivityList(debugActivityList);
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
                    //_ReactionManager.React(result);
                }

                _EvaluateCooldownCoroutine = StartCoroutine(EvaluateCooldown());
            }
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

    }
}
