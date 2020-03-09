using UnityEngine;

namespace Librarian
{
    public class PickupableItem : InteractableItem
    {
        private PickupableBody _Body;

        public PickupableItem(PickupableBody body) : base(body)
        {
            _Body = body;
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