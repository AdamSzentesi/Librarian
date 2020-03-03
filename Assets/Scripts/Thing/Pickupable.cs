using UnityEditor;
using UnityEngine;

namespace Librarian
{
    public abstract class Pickupable : Interactable
    {
        [SerializeField]
        private Item _Item;

        [SerializeField, HideInInspector]
        private string _PrefabPath;

        private bool _IsInitialized = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            bool parentObject = PrefabUtility.IsPartOfPrefabAsset(gameObject);

            if (parentObject)
            {
                GameObject prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource<GameObject>(gameObject);
                string path = AssetDatabase.GetAssetPath(prefab);

                if (path != string.Empty)
                {
                    _PrefabPath = path.Substring(17, path.Length - 24);
                }
            }
        }
#endif

        public void Init(Item item)
        {
            if (_IsInitialized) return;

            _Item = item;
            _IsInitialized = true;
        }

        private void Start()
        {
            if (!_IsInitialized)
            {
                CreateItem(_Item);
                _IsInitialized = true;
            }
        }

        protected abstract void CreateItem(Item itemToSet);

        public sealed override float GetBonus(Feeling feeling)
        {
            return _Item.GetBonus(feeling);
        }

        public sealed override bool Activate(Character character)
        {
            if (!character) return false;
            
            Deactivate(character);

            return character.PickItem(_Item);
        }

        public sealed override bool Deactivate(Character character)
        {
            return true;
        }

        public void Selfdestruct()
        {
            Destroy(gameObject);
        }

    }
}
