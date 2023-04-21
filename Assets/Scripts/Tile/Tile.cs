using UnityEngine;

namespace Tile
{
    public class Tile : Instancable<Tile>
    {
        public  Vector2Int index;
        public int Item;
        public bool isGroup = false, isDestroy = false;
        [SerializeField] private Sprite[] tileSprites;
        private Rigidbody2D _rigidbody;
        private Vector3 _targetPosition;
        private int _maxItem;
        private void Awake()=> _rigidbody = GetComponent<Rigidbody2D>();

        public void SetTile(int _itemChoose,Vector2Int _newLocation)
        {
            index = _newLocation;
            _maxItem = _itemChoose;
            ChangeItemRandom();
            _targetPosition = transform.position;
        }
        public void ChangeItemRandom()
        {
            Item = Random.Range(0, _maxItem);
            UpdateSprite();
        }
        public void UpdateSprite()=>GetComponent<SpriteRenderer>().sprite = tileSprites[Item];
        public void SetGroup(bool status)=> isGroup = status;

    }
}
