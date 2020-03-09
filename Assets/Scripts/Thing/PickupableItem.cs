using UnityEngine;

namespace Librarian
{
    public class PickupableItem : InteractableItem
    {
        public Sprite Icon;
        public PickupableBody Body;
        public Sprite MainSprite { get; private set; }
        public Sprite TopSprite { get; private set; }
        public Sprite BottomSprite { get; private set; }

        public PickupableItem(PickupableSeed seed, PickupableBody body) : base(seed)
        {
            Body = body;

            if (Body)
            {
                MainSprite = Body.MainSprite;
                TopSprite = Body.TopSprite;
                BottomSprite = Body.BottomSprite;
            }

            Icon = seed.Icon;
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
            if (Body) Body.Despawn();
            Body = null;
        }

        public override void DebugMe()
        {
            base.DebugMe();

            Debug.Log(" - Body: " + Body);
            Debug.Log(" - MainSprite: " + MainSprite);
            Debug.Log(" - TopSprite: " + TopSprite);
            Debug.Log(" - BottomSprite: " + BottomSprite);
            Debug.Log(" - Icon: " + Icon);
        }

    }
}