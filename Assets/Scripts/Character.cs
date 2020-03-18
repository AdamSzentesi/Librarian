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
        [Header("Character Settings")]

        public PickupableBody DebugItem;
        public GameObject DebugItemBodyPrefab;

        public string Name;
        public TextMesh Stats;

        [SerializeField] private float WalkSpeed = 1.0f;

        public float RunSpeed = 4.0f;

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

        private CharacterInteface CharacterInteface;

        protected void Start()
        {
            _StateBehaviors[(int)State.Idle] = IdleBehavior;
            _StateBehaviors[(int)State.Walk] = WalkBehavior;
            _StateBehaviors[(int)State.Run] = RunBehavior;
            _StateBehaviors[(int)State.Sit] = SitBehavior;

            // SETUP FEELINGS
            _FeelingManager = new FeelingManager(StartingFeelings);

            // SETUP
            CharacterInteface = new CharacterInteface(this);
        }

        private void Update()
        {
            _FeelingManager.Update();

            // TODO: feeling buffs

            EvaluateFeelings();

            CharacterInteface.Update(WalkSpeed, transform);

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
                    CharacterInteface.React(result);
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
        private Character _Character;
        private ActivityManager _ActivityManager;
        private State _CurrentState;
        private InteractableBody Target;
        private Action _OnTargetReached;
        public Vector3 Position { get { return _Character.transform.position; } }

        public CharacterInteface(Character character)
        {
            _Character = character;
            _ActivityManager = new ActivityManager();
        }

        public void Update(float walkSpeed, Transform transform)
        {
            if (_CurrentState == State.Walk)
            {
                Vector3 direction = Target.transform.position - transform.position;
                if (direction.sqrMagnitude < 1.0f)
                {
                    TargetReached();
                    //Target.Activate(_Character);
                    _CurrentState = State.Idle;
                }
                else
                {
                    direction.Normalize();
                    Vector3 velocity = direction * walkSpeed;
                    velocity *= Time.deltaTime;
                    transform.position += velocity;
                }
            }

            /*
            if (Input.GetKeyUp(KeyCode.Space) && _Character.DebugItem)
            {
                _Character.DebugItem.Activate(_Character);
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                DropItem();
            }
            */
        }

        public void React(Feeling feeling)
        {
            // DEBUG
            ActivityList debugActivityList = new ActivityList();
            FindTargetActivity find = new FindTargetActivity(Feeling.Fun);
            debugActivityList.AddActivity(find);
            debugActivityList.AddActivity(new GoToTargetActivity(find));
            debugActivityList.AddActivity(new ActivateTargetActivity(find));
            _ActivityManager.AddActivityList(debugActivityList, this);
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
            return _ActivityManager.DeactivateTarget(target, _Character);
        }

        public void GoToTarget(InteractableBody target, Action onTargetReached)
        {
            Target = target;

            if (Target != null)
            {
                _OnTargetReached += onTargetReached;
                _CurrentState = State.Walk;
            }
        }

        private void TargetReached()
        {
            if (_OnTargetReached != null)
            {
                _OnTargetReached.Invoke();
            }
        }

        public bool PickItem(PickupableItem item)
        {
            return _ActivityManager.PickItem(item);
        }

        public bool DropItem()
        {
            return _ActivityManager.DropItem(_Character.DebugItemBodyPrefab, Position);
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _Character.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _Character.StopCoroutine(coroutine);
        }

    }

}
