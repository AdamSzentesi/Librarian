using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public class Level : MonoBehaviour
    {
        private static Level _Instance;
        public List<InteractableBody> _Interactables = new List<InteractableBody>();

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

        public static int RegisterInteractable(InteractableBody interactable)
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

        public static InteractableBody GetNearestInteractableBody(Vector3 characterPosition, Feeling feelingBonus)
        {
            if (_Instance._Interactables.Count == 0) return null;

            InteractableBody nearestBody = null;

            int maxIndex = _Instance._Interactables.Count - 1;
            int j = maxIndex;

            for (int i = 0; i < _Instance._Interactables.Count; i++)
            {
                if (HasPositiveFeelingBonus(feelingBonus, _Instance._Interactables[i]))
                {
                    nearestBody = _Instance._Interactables[i];
                    j = i;
                    break;
                }
            }

            if (j == maxIndex)
            {
                return nearestBody;
            }

            float nearestDistanceSquared = (characterPosition - nearestBody.transform.position).sqrMagnitude;

            for (int i = j + 1; i < _Instance._Interactables.Count; i++)
            {
                InteractableBody currentBody = _Instance._Interactables[i];

                float currentDistanceSquared = (characterPosition - currentBody.transform.position).sqrMagnitude;

                if (currentDistanceSquared < nearestDistanceSquared)
                {
                    if (HasPositiveFeelingBonus(feelingBonus, currentBody))
                    {
                        nearestDistanceSquared = currentDistanceSquared;
                        nearestBody = currentBody;
                    }
                }
            }

            return nearestBody;
        }

        private static bool HasPositiveFeelingBonus(Feeling feeling, InteractableBody body)
        {
            return (body.GetBonus(feeling) > 0);
        }

    }
}