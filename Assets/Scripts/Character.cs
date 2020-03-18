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

        public CharacterInteface CharacterInteface { get; private set; }

        protected void Start()
        {
            _StateBehaviors[(int)State.Idle] = IdleBehavior;
            _StateBehaviors[(int)State.Walk] = WalkBehavior;
            _StateBehaviors[(int)State.Run] = RunBehavior;
            _StateBehaviors[(int)State.Sit] = SitBehavior;

            // SETUP FEELINGS
            _FeelingManager = new FeelingManager(StartingFeelings);

            // SETUP
            ActivityManager activityManager = new ActivityManager(this);
            CharacterInteface = new CharacterInteface(activityManager);
        }

        private void Update()
        {
            _FeelingManager.Update();

            // TODO: feeling buffs

            EvaluateFeelings();

            CharacterInteface.Update();

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

    public class CharacterInteface
    {
        private ActivityManager _ActivityManager;
        public Vector3 Position { get { return _ActivityManager.Position; } }

        public CharacterInteface(ActivityManager activityManager)
        {
            _ActivityManager = activityManager;

            // DEBUG
            ActivityList debugActivityList = new ActivityList();
            FindTargetActivity find = new FindTargetActivity(Feeling.Fun);
            debugActivityList.AddActivity(find);
            debugActivityList.AddActivity(new GoToTargetActivity(find));
            debugActivityList.AddActivity(new ActivateTargetActivity(find));
            _ActivityManager.AddActivityList(debugActivityList, this);
        }

        public void Update()
        {
            _ActivityManager.Update();
        }

        // ACTIVITIES

        public void AddActivity(Activity activity, int activityListIndex)
        {
            _ActivityManager.AddActivity(activity, activityListIndex);
        }

        public bool ActivateTarget(InteractableBody target, int activityListIndex)
        {
            return _ActivityManager.ActivateTarget(target, this, activityListIndex);
        }

        public bool DeactivateTarget(InteractableBody target)
        {
            return _ActivityManager.DeactivateTarget(target);
        }

        public void GoToTarget(InteractableBody target, Action onTargetReached)
        {
            _ActivityManager.GoToTarget(target, onTargetReached);
        }

        public bool PickItem(PickupableItem item)
        {
            return _ActivityManager.PickItem(item);
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _ActivityManager.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _ActivityManager.StopCoroutine(coroutine);
        }

    }

}
