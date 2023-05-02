using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Level;

public class SpawnTile : Instancable<SpawnTile>
{
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private RectTransform spawnParent;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public void CreateSpawnManager()
    {
        spawnPoints = new List<GameObject>();
        for (int i = 0; i < LevelManager.instance.CurrentLevelData.Size.x; i++)
        { 
            spawnPoints.Add(Instantiate(spawnPointPrefab,new Vector3(i, LevelManager.instance.CurrentLevelData.Size.y),Quaternion.identity,spawnParent));
        }
    }
}
