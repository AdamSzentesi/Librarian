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

    public class Character : Billboard
    {
        public SceneItem DebugItem;

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

        public Vector3 TargetPosition;
        private Item _Item;


        [SerializeField]
        private FeelingManager _FeelingManager = new FeelingManager();

        protected override void Init()
        {
            _StateBehaviors[(int)State.Idle] = IdleBehavior;
            _StateBehaviors[(int)State.Walk] = WalkBehavior;
            _StateBehaviors[(int)State.Run] = RunBehavior;
            _StateBehaviors[(int)State.Sit] = SitBehavior;

            _FeelingReactions[(int)Feeling.Boredom] = BoredomReaction;
            _FeelingReactions[(int)Feeling.Fear] = FearReaction;
            _FeelingReactions[(int)Feeling.Tiredness] = TirednessReaction;

            _FeelingManager.Init();
        }

        private void Update()
        {
            _FeelingManager.Update();

            // TODO: feeling buffs



            float boredom = _FeelingManager.GetFeeling(Feeling.Boredom);
            float fear = _FeelingManager.GetFeeling(Feeling.Fear);
            float tiredness = _FeelingManager.GetFeeling(Feeling.Tiredness);

            Stats.text = "B: " + boredom + "\nF: " + fear + "\nT: " + tiredness;

            // DEBUG
            if (Input.GetKeyUp(KeyCode.Space) && DebugItem)
            {
                PickItem(DebugItem.Item);
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

        public bool PickItem(Item item)
        {
            if (item == null || _Item != null) return false;

            _Item = item;
            item.DestroyInstance();

            return true;
        }

        public bool DropItem()
        {
            if (_Item == null) return false;

            _Item.CreateInstance(_Item);
            _Item = null;

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

    }
}
