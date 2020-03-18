using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class ActivityManager
    {
        private Character _Character;
        private PickupableItem _Inventory;
        private State _CurrentState;

        public InteractableBody Target;
        public Vector3 Position { get { return _Character.transform.position; } }
        private List<ActivityList> _ActivityLists = new List<ActivityList>();

        public ActivityManager(Character character)
        {
            _Character = character;
        }

        public void Update()
        {
            /*
            if (_Activities.Count > 0)
            {
                _Activities[0].Start(this, null);
            }
            */

            if (_CurrentState == State.Walk)
            {
                Vector3 direction = Target.transform.position - Position;
                if (direction.sqrMagnitude < 1.0f)
                {
                    TargetReached();
                    //Target.Activate(_Character);
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

        public void AddActivityList(ActivityList activityList, CharacterInteface characterInteface)
        {
            if (activityList == null) return; // TODO: or empty?

            Debug.Log("ActivityManager: AddActivityList");

            int activityListIndex = _ActivityLists.Count;
            _ActivityLists.Add(activityList);
            activityList.Start(characterInteface, activityListIndex);
        }

        public void AddActivity(Activity activity, int activityListIndex)
        {
            if (_ActivityLists[activityListIndex] == null) return;
            
            _ActivityLists[activityListIndex].AddActivity(activity);
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _Character.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                _Character.StopCoroutine(coroutine);
            }
        }

        // ACTIVITIES

        private Action _OnTargetReached;
        private void TargetReached()
        {
            if (_OnTargetReached != null)
            {
                _OnTargetReached.Invoke();
            }
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

        public bool ActivateTarget(InteractableBody target, CharacterInteface characterInterface, int activityListIndex)
        {
            Debug.Log("ActivateTarget " + target);
            if (target) return target.Activate(characterInterface, activityListIndex);

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
        
    }
}