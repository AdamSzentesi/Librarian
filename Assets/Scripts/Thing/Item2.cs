using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Librarian
{
    public abstract class Item2
    {
        private string _PrefabPath;
        private Pickupable _Owner;
        
        protected float[] _Bonuses;

        public Item2(Pickupable owner, string prefabPath)
        {
            _Owner = owner;
            _PrefabPath = prefabPath;
            _Bonuses = new float[Enum.GetNames(typeof(Feeling2)).Length];
        }

        public abstract float GetBonus(Feeling2 feeling);

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