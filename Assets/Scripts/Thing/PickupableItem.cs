using UnityEngine;

namespace Librarian
{
    public class PickupableItem : InteractableItem
    {
        private PickupableBody _Body;

        public Sprite MainSprite { get; private set; }
        public Sprite TopSprite { get; private set; }
        public Sprite BottomSprite { get; private set; }

        public PickupableItem(PickupableBody body) : base()
        {
            _Body = body;

            if (_Body)
            {
                MainSprite = _Body.MainSprite;
                TopSprite = _Body.TopSprite;
                BottomSprite = _Body.BottomSprite;
            }
        }

        public void Spawn(GameObject prefab, Vector3 position)
        {
            GameObject instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;

            PickupableBody sceneItem = instance.GetComponent<PickupableBody>();
            if (sceneItem) sceneItem.Init(this);
        }

        public void Despawn()
        {
            if (_Body) _Body.Despawn();
        }

    }
}