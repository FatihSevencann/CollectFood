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

    private GameObject[,] _tiles;
    private bool _shuffleCheck = false;

    public void CreateTileSet()
    {
        if (LevelManager.instance.CurrentLevelData == null)
            return;

        _tiles = new GameObject[LevelManager.instance.CurrentLevelData.Size.x,
            LevelManager.instance.CurrentLevelData.Size.y];

        gridLayoutGroup.constraintCount = LevelManager.instance.CurrentLevelData.Size.x;

        float cellSize = (float)FindObjectOfType<CanvasScaler>().referenceResolution.x * 0.70f /
                         LevelManager.instance.CurrentLevelData.Size.x;

        gridLayoutGroup.cellSize = Vector2.one * cellSize;

        for (int i = 0; i < LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
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

    void Shuffle()
    {
        for (int i = 0; i < LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
            {
                _tiles[i, j].GetComponent<TileManager>().ChangeItemRandom();
            }
        }
    }

    void IsGroupControl()
    {
        _shuffleCheck = true;
        for (int i = 0; i < LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
            {
                if (_tiles[i, j].GetComponent<TileManager>().isGroup == false)
                {
                    CheckForNeighbours(i, j, _tiles[i, j].GetComponent<TileManager>().Item);
                }
            }
        }

        if (_shuffleCheck)
        {
            Shuffle();
        }
    }

    void CheckForNeighbours(int x, int y, int Item)
    {
        
    }

}
        
