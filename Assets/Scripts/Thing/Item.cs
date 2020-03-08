using System;
using UnityEngine;

namespace Librarian
{
    public class Item
    {
        private PickupableItemBody _Body = null;
        protected float[] _Bonuses;

        public Sprite MainSprite { get; private set; }
        public Sprite TopSprite { get; private set; }
        public Sprite BottomSprite { get; private set; }

        public Item(PickupableItemBody body)
        {
            _Body = body;

            if (_Body)
            {
                MainSprite = _Body.MainSprite;
                TopSprite = _Body.TopSprite;
                BottomSprite = _Body.BottomSprite;
            }

            _Bonuses = new float[Enum.GetNames(typeof(Feeling)).Length];
        }

        public float GetBonus(Feeling feeling)
        {
            return _Bonuses[(int)feeling];
        }

        public void Spawn(GameObject prefab, Vector3 position)
        {
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;
            
            PickupableItemBody sceneItem = instance.GetComponent<PickupableItemBody>();
            if (sceneItem) sceneItem.Init(this);
        }

        public void Despawn()
        {
            if (_Body) _Body.Despawn();
        }

    }
}