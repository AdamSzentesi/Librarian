using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class Level : MonoBehaviour
    {
        private static Level _Instance;
        public List<InteractableItemBody> _Interactables = new List<InteractableItemBody>();

        public Transform CameraBase;

        private void Awake()
        {
            if (_Instance)
            {
                Destroy(this);
                return;
            }

            _Instance = this;

            if (!CameraBase)
            {
                Debug.LogError("Level: No CameraBase reference!");
                _CameraRotation = Quaternion.Euler(45, -45, 0);
                return;
            }
            else
            {
                Vector3 rotation = CameraBase.rotation.eulerAngles;
                //rotation.x = 0;
                _CameraRotation = Quaternion.Euler(rotation);
            }
        }

        public static Level Instance
        {
            get
            {
                return _Instance;
            }
        }

        private static Quaternion _CameraRotation;
        public static Quaternion CameraRotation
        {
            get
            {
                return _CameraRotation;
            }
        }

        public static int RegisterInteractable(InteractableItemBody interactable)
        {
            if (!interactable) return -1;

            int result = _Instance._Interactables.Count;
            _Instance._Interactables.Add(interactable);

            return result;
        }

        public static void UnregisterInteractable(int index)
        {
            if (index < 0 || index >= _Instance._Interactables.Count) return;

            _Instance._Interactables.RemoveAt(index);
        }

        public static Vector3 GetNearestInteractablePosition(Vector3 characterPosition)
        {
            if (_Instance._Interactables.Count == 0) return characterPosition;

            Vector3 closestPosition = _Instance._Interactables[0].transform.position;
            if (_Instance._Interactables.Count == 1) return closestPosition;

            float closestDistanceSquared = (characterPosition - closestPosition).sqrMagnitude;
            foreach (InteractableItemBody sceneItem in _Instance._Interactables)
            {
                float currentDistanceSquared = (characterPosition - sceneItem.transform.position).sqrMagnitude;
                if (currentDistanceSquared < closestDistanceSquared)
                {
                    closestDistanceSquared = currentDistanceSquared;
                    closestPosition = sceneItem.transform.position;
                }
            }

            return closestPosition;
        }

    }
}