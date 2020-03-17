using System;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class ActivityManager
    {
        private List<Activity> _Activities = new List<Activity>();
        private Character _Character;
        private PickupableItem _Inventory;
        private State _CurrentState;

        public InteractableBody Target;
        public Vector3 Position { get { return _Character.transform.position; } }

        public ActivityManager(Character character)
        {
            _Character = character;
        }

        public void Update()
        {
            if (_Activities.Count > 0)
            {
                _Activities[0].Begin(this, null);
            }


            /*
            if (_CurrentState == State.Walk)
            {
                Vector3 direction = Target.transform.position - Position;
                if (direction.sqrMagnitude < 1.0f)
                {
                    Target.Activate(_Character);
                    _CurrentState = State.Idle;
                }
                else
                {
                    direction.Normalize();
                    Vector3 velocity = direction * _Character.WalkSpeed;
                    velocity *= Time.deltaTime;
                    _Character.transform.position += velocity;
                }
            }

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

        public bool AddActivity(Activity activity)
        {
            Debug.Log("ADDING ACTIVITY: " + activity);
            _Activities.Add(activity);
            return true;
        }

        // ACTIVITIES

        public void GoToTarget(InteractableBody target)
        {
            Target = target;
            if (target != null) _CurrentState = State.Walk;
        }

        public bool ActivateTarget(InteractableBody target)
        {
            if (target) return target.Activate(_Character);

            return false;
        }

        public bool DeactivateTarget(InteractableBody target)
        {
            if (target) return target.Deactivate(_Character);

            return false;
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

            _Inventory.Spawn(_Character.DebugItemBodyPrefab, Position);
            _Inventory = null;

            return true;
        }

        private bool[] _FeelingsBeingInvolved = new bool[Enum.GetValues(typeof(Feeling)).Length];
        public void ToggleFeelingBeingInvolved(Feeling feeling, bool status)
        {
            _FeelingsBeingInvolved[(int)feeling] = status;
        }

    }
}