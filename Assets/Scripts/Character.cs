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

        public InteractableBody Target;
        private State _CurrentState;
        private PickupableItem _Inventory;

        public Feelings StartingFeelings;

        [SerializeField]
        private FeelingManager _FeelingManager = new FeelingManager();

        private List<Activity> _Activities = new List<Activity>();

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
        }

        private void Update()
        {
            _FeelingManager.Update();

            // TODO: feeling buffs

            EvaluateFeelings();

            if (_CurrentState == State.Walk)
            {
                Vector3 direction = Target.transform.position - transform.position;
                if (direction.sqrMagnitude < 1.0f)
                {
                    Target.Activate(this);
                    _CurrentState = State.Idle;
                }
                else
                {
                    direction.Normalize();
                    Vector3 velocity = direction * WalkSpeed;
                    velocity *= Time.deltaTime;
                    transform.position += velocity;
                }
            }

            // DEBUG
            float fun = _FeelingManager.GetFeeling(Feeling.Fun);
            float calm = _FeelingManager.GetFeeling(Feeling.Calm);
            float fresh = _FeelingManager.GetFeeling(Feeling.Fresh);

            Stats.text = "FUN: " + fun + "\nCALM: " + calm + "\nFRESH: " + fresh;
            
            if (Input.GetKeyUp(KeyCode.Space) && DebugItem)
            {
                DebugItem.Activate(this);
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                DropItem();
            }

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

        public bool PickItem(PickupableItem item)
        {
            if (item == null || _Inventory != null) return false;

            _Inventory = item;
            _Inventory.Despawn();
            
            return true;
        }

        public bool DropItem()
        {
            if (_Inventory == null) return false;

            _Inventory.Spawn(DebugItemBodyPrefab, transform.position);
            _Inventory = null;

            return true;
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

        public void AddActivity(Activity activity)
        {
            if (activity == null) return;

            _Activities.Add(activity);
        }

        // Activities

        public void GoToTarget(InteractableBody target)
        {
            Target = target;
            if (Target != null) _CurrentState = State.Walk;
        }

        public bool ActivateTarget(InteractableBody target)
        {
            if (target) return target.Activate(this);

            return false;
        }

        public bool DeactivateTarget(InteractableBody target)
        {
            if (target) return target.Deactivate(this);

            return false;
        }

    }
}
