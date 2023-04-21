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

    public void CreateTileSet()
    {
        if (LevelManager.instance.CurrentLevelData == null)
            return;
        
        gridLayoutGroup.constraintCount = LevelManager.instance.CurrentLevelData.Size.x;

        float cellSize = (float)Screen.width / LevelManager.instance.CurrentLevelData.Size.x;

        gridLayoutGroup.cellSize = new Vector2(cellSize, cellSize);
        
        for (int i = 0; i <LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
            {
                GameObject tile = Instantiate(tileManager, new Vector3(i, j), quaternion.identity, spawnParent);
                Vector2Int tileLocalization = new Vector2Int(i, j);
                tile.GetComponent<TileManager>().SetTile(8,tileLocalization);
                tile.SetActive(true);
            }
        }
    }
}
