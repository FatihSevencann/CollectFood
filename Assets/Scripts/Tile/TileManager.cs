using System;
using Level;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Level;

public class TileManager : Instancable<TileManager>
{
        public Sprite[] tileSprites;
        public  Vector2Int index;
        public int _item;
        public bool isGroup;
        public bool isDestroy;
        public Vector3 _targetPosition;
        
        [SerializeField] private Image tileImage;
        [SerializeField] private int moveDownLenght = 0;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        
        private int _maxItem;
        private bool _move = false;

     

        private void Awake()=> _rigidbody = GetComponent<Rigidbody2D>();

        public void SetTile(int _itemCount,Vector2Int _newLocation)
        {
            index = _newLocation;
            _maxItem = _itemCount;
            ChangeItemRandom();
            _targetPosition = transform.position;
        }
        public void ChangeItemRandom()
        {
            _item = Random.Range(0, _maxItem);
            UpdateSprite();
        }
        public void ChangeItem(int item)
        {
            _item = item;
            UpdateSprite();
        }
        public void UpdateSprite() =>tileImage.sprite = tileSprites[_item];
        public void SetGroup(bool status)=> isGroup = status;
        public void MoveTileDown()
        {
            _targetPosition += Vector3.down;
            moveDownLenght++;
        }
        public void StartMovingTile()
        {
            index += Vector2Int.down * moveDownLenght;
            GridManager.instance.AssingTiles(gameObject, index);
            moveDownLenght = 0;
            _move = true;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        private void FixedUpdate()
        {
            if (_move)
            {
                if (transform.position.y <= _targetPosition.y)
                {
                    _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                    _move = false;
                    transform.position = _targetPosition;
                }
            }
        }
        public void ReuseTile(Vector3 newPos, Vector2Int targetPos)
        {
            moveDownLenght = 0;
            transform.position = newPos;
            index = targetPos;
            _targetPosition = new Vector3(targetPos.x, targetPos.y);
            ChangeItemRandom();
            StartMovingTile();
        }
}

