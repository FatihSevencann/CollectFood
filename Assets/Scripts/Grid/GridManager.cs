using System;
using System.Collections;
using System.Collections.Generic;
using Level;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridManager : Instancable<GridManager>
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private RectTransform tileParent;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    
    private GameObject[,] _tiles;
    private bool _shuffleCheck = false;

    
    public List<GameObject> matchNeighbours = new List<GameObject>();
    public List<GameObject> nextChecks = new List<GameObject>();

   

    public void ResponsiveGrid( )
    {
       
        _gridLayoutGroup.constraintCount = LevelManager.instance.CurrentLevelData.Size.x;

        float cellSize = (float)FindObjectOfType<CanvasScaler>().referenceResolution.x *0.90f /  LevelManager.instance.CurrentLevelData.Size.x;

        _gridLayoutGroup.cellSize = Vector2.one * cellSize;
    }
    
    
    
    public void CreateTileSet()
    {
        
        
            if (LevelManager.instance.CurrentLevelData == null)
            {
                return;
            }
            ResponsiveGrid();
            _tiles = new GameObject[ LevelManager.instance.CurrentLevelData.Size.x,  LevelManager.instance.CurrentLevelData.Size.y];
            
            for (int i = 0; i < LevelManager.instance.CurrentLevelData.Size.x; i++)
            {
                for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
                {
                    GameObject tile  = Instantiate(tilePrefab,new Vector3(i,j),quaternion.identity, tileParent);
                    Vector2Int tileLocalization = new Vector2Int(i, j);
                    tile.GetComponent<TileManager>().SetTile(8, tileLocalization);
                    _tiles[i, j] = tile;
                    tile.SetActive(true);
                }
            }
            IsGroupControl();
        }
        
    
    void IsGroupControl()
    {
        _shuffleCheck = true;
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j <1 ; j++)
            {
                if (!_tiles[i, j].GetComponent<TileManager>().isGroup)
                {

                    CheckForNeighbours(i, j, _tiles[i, j].GetComponent<TileManager>()._item);
                }
            }
        }
        if (_shuffleCheck)
        {
            // Shuffle();
        }
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
    //     IsGroupControl();
    // }
    Vector2 NextNeighbours(ref int x, ref int y)
    {
        if (nextChecks.Count > 0)
        {
            x = nextChecks[0].GetComponent<TileManager>().index.x;
            y = nextChecks[0].GetComponent<TileManager>().index.y;
            nextChecks.RemoveAt(0);
            return new Vector2(x, y);
        }
        else
        {
            if (x + 1 %  LevelManager.instance.CurrentLevelData.Size.x == 0)
                return new Vector2(0, y + 1);
            else
                return new Vector2(x + 1, y);
        }
    }
    void NeighboursAdd(GameObject itemFirst, GameObject item)
    {
        if (!itemFirst.GetComponent<TileManager>().isGroup)
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
                    NeighboursAdd(_tiles[x, y], _tiles[x - 1, y]);
                }
                else
                {
                    _tiles[x - 1, y].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }
            //right check
            if (x <  LevelManager.instance.CurrentLevelData.Size.x - 1 && !_tiles[x + 1, y].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x + 1, y].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x + 1, y]);
                }
                else
                {
                    _tiles[x + 1, y].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }
            //LeftUp Check
            if (x > 0 && y < LevelManager.instance.CurrentLevelData.Size.y-1 && !_tiles[x - 1, y + 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x - 1, y + 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x - 1, y + 1]);
                }
                else
                {
                    _tiles[x - 1, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }
            //RightUp Check
            if (x <  LevelManager.instance.CurrentLevelData.Size.x - 1 && y< LevelManager.instance.CurrentLevelData.Size.y-1 && !_tiles[x + 1, y + 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x + 1, y + 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x + 1, y + 1]);
                }
                else
                {
                    _tiles[x + 1, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }
            //LeftDown
            if (x > 0 && y>0 && !_tiles[x - 1, y - 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x - 1, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x - 1, y - 1]);
                }
                else
                {
                    _tiles[x - 1, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }
            //RightDown
            if (x <  LevelManager.instance.CurrentLevelData.Size.x - 1 && y>0 && !_tiles[x + 1, y - 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x + 1, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x + 1, y - 1]);
                }
                else
                {
                    _tiles[x + 1, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }

            //up check
             if (y < LevelManager.instance.CurrentLevelData.Size.y-1 && !_tiles[x, y + 1].GetComponent<TileManager>().isGroup)
             {
                 if (!foundNeighbour && _tiles[x, y + 1].GetComponent<TileManager>()._item == Item)
                 {
                     NeighboursAdd(_tiles[x, y], _tiles[x, y + 1]);
                 }
                 else
                 {
                     _tiles[x, y + 1].GetComponent<TileManager>().ChangeItem(Item);
                     return;
                 }
             }

            //downcheck
            if (y>0 && !_tiles[x, y - 1].GetComponent<TileManager>().isGroup)
            {
                if (!foundNeighbour && _tiles[x, y - 1].GetComponent<TileManager>()._item == Item)
                {
                    NeighboursAdd(_tiles[x, y], _tiles[x, y - 1]);
                }
                else
                {
                    _tiles[x, y - 1].GetComponent<TileManager>().ChangeItem(Item);
                    return;
                }
            }

            if (!_tiles[x, y].GetComponent<TileManager>().isGroup)
            {
                _tiles[x, y].GetComponent<TileManager>().UpdateSprite();
            }

            foundNeighbour = matchNeighbours.Count > 1;

            Vector2 nextVector = NextNeighbours(ref x, ref y);
            x = (int)nextVector.x;
            y = (int)nextVector.y;
            
            run = matchNeighbours.Count <= 2;
        }
    }

    public void FindDestroyObjects()
    {
        foreach (GameObject gameObject in LineManager._selectedObjects)
        {
            Vector2Int destroyIndex = gameObject.GetComponent<TileManager>().index;
            for (int y = destroyIndex.y; y > 0; y--)
            {
                _tiles[destroyIndex.x, y - 1].GetComponent<TileManager>().MoveTileDown();
            }
        }
        foreach (GameObject gameObject in LineManager._selectedObjects)
        {
            DestroyTile(gameObject);
        }

        foreach (GameObject gameObject in _tiles)
         {
             gameObject.GetComponent<TileManager>().StartMovingTile();
         }

        SpawnTiles();
        RefreshTile();
    }

    public void AssingTiles(GameObject Tile, Vector2Int index)
    {
        _tiles[index.x, index.y] = Tile;
    }

    public void DestroyTile(GameObject tile)
    {
        tile.SetActive(false);
        SpawnTile.instance.spawnPoints[tile.GetComponent<TileManager>().index.x].GetComponent<Spawner>().AddToSpawnList(tile);
    }

    public void SpawnTiles()
    {
        for (int i = 0; i <  LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            SpawnTile.instance.spawnPoints[i].GetComponent<Spawner>().StartSpawning();
        }
    }

    public void RefreshTile()
    {
        for (int i = 0; i <  LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j <  LevelManager.instance.CurrentLevelData.Size.y; j++)
            {
                _tiles[i, j].GetComponent<TileManager>().isGroup = false;
            }
        }

        IsGroupControl();
    }
}