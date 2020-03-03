using System;
using UnityEngine;

namespace Librarian
{
    public abstract class Item
    {
        private string _PrefabPath = string.Empty;
        private Pickupable _Owner = null;
        
        protected float[] _Bonuses;

        public Item()
        {
        }

        public Item(Pickupable owner, string prefabPath)
        {
            _Owner = owner;
            _PrefabPath = prefabPath;
            _Bonuses = new float[Enum.GetNames(typeof(Feeling)).Length];
        }

        public abstract float GetBonus(Feeling feeling);

        public void CreateThing(Vector3 position)
        {
            GameObject instance = PrefabManager.CreateInstance(_PrefabPath);
            instance.transform.position = position;
            Pickupable sceneItem = instance.GetComponent<Pickupable>();
            
            if (sceneItem)
            {
                sceneItem.Init(this);
            }
        }

        public void DestroyThing()
        {
            if (!_Owner) return;

            _Owner.Selfdestruct();
        }

    }
}