using UnityEditor;
using UnityEngine;

namespace Librarian
{
    public class SceneItem : MonoBehaviour
    {
        [Range(-100, 100)]
        public float BoredomBonus;
        [Range(-100, 100)]
        public float FearBonus;
        [Range(-100, 100)]
        public float TirednessBonus;

        public Collider Trigger;
        public Collider Obstacle;
        public bool IsBillboard = true;

        public virtual bool Activate() { return false; }
        public virtual bool Deactivate() { return true; }

        [SerializeField]
        private Item _Item;
        public Item Item
        {
            get
            {
                return _Item;
            }
            set
            {
                if (value != null)
                {
                    _Item = value;
                    UpdateStats();
                }
            }
        }

        [SerializeField, HideInInspector]
        private string _PrefabPath;

        private int _RegisteredIndex = -1;

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

        protected void Awake()
        {
            _RegisteredIndex = Level.RegisterInteractable(this);
        }

        protected virtual void Start()
        {
            if (_Item == null)
            {
                _Item = new Item(this, _PrefabPath, BoredomBonus, FearBonus, TirednessBonus);
            }

            if (IsBillboard)
            {
                transform.rotation = Level.CameraRotation;
            }
        }

        private void UpdateStats()
        {
            BoredomBonus = Item.GetBonus(Feeling.Boredom);
            FearBonus = Item.GetBonus(Feeling.Fear);
            TirednessBonus = Item.GetBonus(Feeling.Tiredness);
        }

        public float GetBonus(Feeling feeling)
        {
            return _Item.GetBonus(feeling);
        }

        public void DestroyInstance()
        {
            _Item = null;
            Level.UnregisterInteractable(_RegisteredIndex);
            Destroy(gameObject);
        }

    }
}
