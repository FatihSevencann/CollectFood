using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Level
{
    public class LevelManager : Instancable<LevelManager>
    {
        public List<LevelData> levelDatas;
        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt("currentLevel", 0);
            private set => PlayerPrefs.SetInt("currentLevel", value);
        }
        public LevelData CurrentLevelData { get; set; }
        
        private GameObject _tileManager;
        public UnityAction onLevelLoaded;

        private void Start()
        {
            CurrentLevelData = levelDatas[CurrentLevel];
            
            LoadLevel();
        }

        void LoadLevel()
        {
            // Level yukleme islemleri...
            
            onLevelLoaded += GridManager.instance.CreateTileSet;

            onLevelLoaded();
        }

        void LevelUp()
        {
            CurrentLevel++;
            CurrentLevelData = levelDatas[CurrentLevel];
        }

    
    
    }
}