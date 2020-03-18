using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class ActivityManager
    {
        private PickupableItem _Inventory;
        private List<ActivityList> _ActivityLists = new List<ActivityList>();

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

        // ACTIVITIES

        public bool ActivateTarget(InteractableBody target, CharacterInteface characterInterface, int activityListIndex)
        {
            Debug.Log("ActivateTarget " + target);
            if (target) return target.Activate(characterInterface, activityListIndex);

            return false;
        }

        public bool DeactivateTarget(InteractableBody target, Character character)
        {
            if (target) return target.Deactivate(character);

            return false;
        }

        public bool PickItem(PickupableItem item)
        {
            if (item == null || _Inventory != null) return false;

            _Inventory = item;
            _Inventory.Despawn();

            return true;
        }

        public bool DropItem(GameObject prefab, Vector3 position)
        {
            if (_Inventory == null) return false;

            _Inventory.Spawn(prefab, position);
            _Inventory = null;

            return true;
        }
        
    }
}