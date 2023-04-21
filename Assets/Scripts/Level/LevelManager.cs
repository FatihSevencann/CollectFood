using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Instancable<LevelManager>
{
    [SerializeField] GameObject[,] _tObjects;
    public List<LevelData> _levelDatas;

    public int currentLevel
    {
        get => PlayerPrefs.GetInt("currentLevel", 0);
        private set => PlayerPrefs.SetInt("currentLevel", value);
    }
    private GameObject _tileManager;
    
    public LevelData CurrentLevelData { get; set; }

    private void Start()
    {
        CurrentLevelData = _levelDatas[currentLevel];
    }

    void LevelUp()
    {
        currentLevel++;
        CurrentLevelData = _levelDatas[currentLevel];
    }

    
    
}