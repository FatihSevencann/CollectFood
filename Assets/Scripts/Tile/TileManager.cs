using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TileManager : Instancable<TileManager>
    {
        
        [SerializeField] private Image tileImage;
        
        public Sprite[] tileSprites;
        public  Vector2Int index;
        public int _item;
        public bool isGroup;

       

        public Vector3 _targetPosition;
        private int _maxItem;

        private void Start()
        {
            isGroup = false;
        }

        public void SetTile(int _itemCount,Vector2Int _newLocation)
        {
            index = _newLocation;
            _maxItem = _itemCount;
            _targetPosition = GetComponent<RectTransform>().anchoredPosition;

            ChangeItemRandom();
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

        public void UpdateSprite() => tileImage.sprite = tileSprites[_item];
        public void SetGroup(bool status)=> isGroup = status;
        
        
    }

