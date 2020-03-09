using UnityEngine;

namespace Librarian
{
    public class BasicBody : MonoBehaviour
    {
        [SerializeField]
        protected BasicSeed _BasicSeed;

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

        protected virtual void Awake()
        {
            if (_Billboard) _Billboard.rotation = Level.CameraRotation;
            if (_BasicSeed) gameObject.name = _BasicSeed.Name;
        }

        public void SetupSprites(BasicItem basicItem)
        {
            if (basicItem != null)
            {
                MainSprite = basicItem.MainSprite;
                TopSprite = basicItem.TopSprite;
                BottomSprite = basicItem.BottomSprite;
            }
        }

    }
}