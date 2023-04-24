using System;
using System.Collections;
using System.Collections.Generic;
using Level;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : Instancable<GridManager>
{
    [SerializeField] private GameObject tileManager;
    [SerializeField] private RectTransform spawnParent;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    private int _gridX, _gridY;

    private GameObject[,] _tiles;
    private bool _shuffleCheck = false;

    public List<GameObject> matchNeighbours = new List<GameObject>();
    public List<GameObject> nextChecks = new List<GameObject>();

    public void CreateTileSet()
    {
        _gridX = LevelManager.instance.CurrentLevelData.Size.x;
        _gridY = LevelManager.instance.CurrentLevelData.Size.y;
        if (LevelManager.instance.CurrentLevelData == null)
            return;

        _tiles = new GameObject[_gridX, _gridY];

        gridLayoutGroup.constraintCount = _gridX;

        float cellSize = (float)FindObjectOfType<CanvasScaler>().referenceResolution.x * 0.70f / _gridX;

        gridLayoutGroup.cellSize = Vector2.one * cellSize;
        

        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                GameObject tile = Instantiate(tileManager, new Vector3(i, j), quaternion.identity, spawnParent);
             
                Vector2Int tileLocalization = new Vector2Int(i, j);
                tile.GetComponent<TileManager>().SetTile(8, tileLocalization);
                _tiles[i, j] = tile;
                tile.SetActive(true);
            }
        }

         IsGroupControl();
    }

    // void Shuffle()
    // {
    //     for (int i = 0; i < _gridX; i++)
    //     {
    //         for (int j = 0; j < _gridY; j++)
    //         {
    //             _tiles[i, j].GetComponent<TileManager>().ChangeItemRandom();
    //         }
    //     }
    //     
    //     // IsGroupControl();
    // }

    void IsGroupControl()
    {
        _shuffleCheck = true;
        bool flag = false;
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                
                if (!_tiles[i, j].GetComponent<TileManager>().isGroup)
                {
                    if (!_shuffleCheck)
                    {
                        flag = true;
                        break;
                    }

                    CheckForNeighbours(i, j, _tiles[i, j].GetComponent<TileManager>()._item);
                }
            }
    
            if (flag)
                break;
        }
    
        if (_shuffleCheck)
        {
            // matchNeighbours.Clear();
            // nextChecks.Clear();
           // Shuffle();
            
        }
    }

    // bool NextNeighbours(ref int x, ref int y, int item)
    // {
    //     if (nextChecks.Count > 0)
    //     {
    //         x = nextChecks[0].GetComponent<TileManager>().index.x;
    //         y = nextChecks[0].GetComponent<TileManager>().index.y;
    //         nextChecks.RemoveAt(0);
    //         return true;
    //     }
    //     else
    //     {
    //         if (matchNeighbours.Count >= 3)
    //         {
    //             _shuffleCheck = false;
    //         }
    //
    //         return false;
    //     }
    // }

    void NeighboursAdd(GameObject itemFirst, GameObject item)
    {
        if(!itemFirst.GetComponent<TileManager>().isGroup)
         matchNeighbours.Add(itemFirst);
        
        itemFirst.GetComponent<TileManager>().isGroup = true;
    
        matchNeighbours.Add(item);
        nextChecks.Add(item);
        TileManager tileManager = item.GetComponent<TileManager>();
        if (tileManager != null) tileManager.isGroup = true;
    }
    void CheckForNeighbours(int x, int y, int Item)
    {
        if (matchNeighbours.Count > 0 && matchNeighbours[0].GetComponent<TileManager>()._item != Item)
        {
            matchNeighbours.Clear();
            nextChecks.Clear();
        }
        bool run = true;
        bool foundNeighbour = false;
        
        while (run)
        {
            //leftcheck
            if (x > 0 && !_tiles[x - 1, y].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x - 1, y].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x - 1, y]);
                }
                else
                {
                    _tiles[x - 1, y].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("sola girdi");
                    return;
                }
            }
    
            //right check
            if (x < _gridX - 1 && !_tiles[x + 1, y].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x + 1,y].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x + 1, y]);
                }
                else
                {
                    _tiles[x + 1, y].GetComponent<TileManager>().ChangeItem(Item);
                    print( "x" + x+1 + "y"+y +"saga girdi");
                    _shuffleCheck = false;
                    return;
                }
            }
            //LeftUp Check
            if (x > 0 && y > 0 && _tiles[x - 1, y - 1].GetComponent<TileManager>().isGroup == false )
            {
                if (!foundNeighbour&&_tiles[x - 1, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x - 1, y - 1]);
                }
                else
                {
                    _tiles[x - 1, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("solust girdi");
                    return;
                }
            }
            //RightUp Check
            if (x < _gridX - 1 && y > 0 && !_tiles[x + 1, y - 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x + 1, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x + 1, y - 1]);
                }
                else
                {
                    _tiles[x + 1, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("sagust girdi");
                    return;
                }
            }
            //LeftDown
            if (x > 0 && y < _gridY - 1 && !_tiles[x - 1, y + 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x - 1, y + 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x - 1, y + 1]);
                }
                else
                {
                    _tiles[x - 1, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("sol alt girdi");
                    return;
                }
            }
            //RightDown
            if (x < _gridX - 1 && y < _gridY - 1 && !_tiles[x + 1, y + 1].GetComponent<TileManager>().isGroup )
            {
                if (!foundNeighbour && _tiles[x + 1, y +1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x + 1, y + 1]);
                }
                else
                {
                    _tiles[x + 1, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("sag alt girdi");
                    return;
                }
            }
            //up check
            if (y > 0 && !_tiles[x, y - 1].GetComponent<TileManager>().isGroup )
            {
                if (!foundNeighbour&& _tiles[x, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x, y - 1]);
                }
                else
                {
                    _tiles[x, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("ust girdi");
                    return;
                }
            }
            //downcheck
            if (y < _gridY - 1 && !_tiles[x, y + 1].GetComponent<TileManager>().isGroup )
            {
                if (!foundNeighbour && _tiles[x, y + 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y],_tiles[x, y + 1]);
                }
                else
                {
                    _tiles[x, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                    _shuffleCheck = false;
                    print("alt girdi");
                    return;
                }
            }
            // if (matchNeighbours.Count > 0 && !_tiles[x, y].GetComponent<TileManager>().isGroup)
            // {
            //     matchNeighbours.Add(_tiles[x, y]);
            //     _tiles[x, y].GetComponent<TileManager>().isGroup = true;
            // }
            // if (!_tiles[x, y].GetComponent<TileManager>().isGroup)
            // {
            //     _tiles[x, y].GetComponent<TileManager>().UpdateSprite();
            // }
            //run = NextNeighbours(ref x, ref y, Item);
            foundNeighbour = matchNeighbours.Count > 1;
        }
    }
}