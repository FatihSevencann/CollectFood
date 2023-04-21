using UnityEngine;
using UnityEngine.UI;

    public class TileManager : Instancable<TileManager>
    {
        [SerializeField] private Sprite[] tileSprites;
        [SerializeField] private Image tileImage;
        
        public  Vector2Int index;
        public int Item;
        public bool isGroup = false, isDestroy = false;
        
        
        private Vector3 _targetPosition;
        private int _maxItem;
        
        public void SetTile(int _itemCount,Vector2Int _newLocation)
        {
            index = _newLocation;
            _maxItem = _itemCount;
            ChangeItemRandom();
            _targetPosition = transform.position;
        }
        public void ChangeItemRandom()
        {
            Item = Random.Range(0, _maxItem);
            UpdateSprite();
        }

        public void UpdateSprite() => tileImage.sprite = tileSprites[Item];
        public void SetGroup(bool status)=> isGroup = status;
    }

