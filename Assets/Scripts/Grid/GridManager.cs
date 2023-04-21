using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Instancable<GridManager>
{
   
    public void CreateTileSet()
    {
        for (int i = 0; i <LevelManager.instance.CurrentLevelData.Size.x; i++)
        {
            for (int j = 0; j < LevelManager.instance.CurrentLevelData.Size.y; j++)
            {
               
            }
        }
    }
}
