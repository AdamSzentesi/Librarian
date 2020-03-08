using UnityEngine;

namespace Librarian
{
    public class ItemBody : MonoBehaviour
    {
        [SerializeField]
        protected ItemSeed _ItemSeed;

        [SerializeField]
        private Transform _Billboard;

        [SerializeField]
        private SpriteRenderer _MainSprite;
        public Sprite MainSprite { get { return _MainSprite ? _MainSprite.sprite : null; } private set { if (_MainSprite) _MainSprite.sprite = value; } }

        [SerializeField]
        private SpriteRenderer _TopSprite;
        public Sprite TopSprite { get { return _TopSprite ? _TopSprite.sprite : null; } private set { if (_TopSprite) _TopSprite.sprite = value; } }

        [SerializeField]
        private SpriteRenderer _BottomSprite;
        public Sprite BottomSprite { get { return _BottomSprite ? _BottomSprite.sprite : null; } private set { if (_BottomSprite) _BottomSprite.sprite = value; } }

        public Collider Trigger;
        public Collider Obstacle;

        //private void OnValidate()
        //{
        //    SetupSprites();
        //}

        protected virtual void Awake()
        {
            if (_Billboard) _Billboard.rotation = Level.CameraRotation;

            //SetupSprites();
        }

        public void SetupSprites(Item item)
        {
            if (item != null)
            {
                MainSprite = item.MainSprite;
                TopSprite = item.TopSprite;
                BottomSprite = item.BottomSprite;
            }
        }

    }
}