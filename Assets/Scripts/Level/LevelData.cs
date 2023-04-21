using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
public enum ItemTypes
{
    Apple,Pumpkin,Banana,YellowedLeaf,GreenLeaf
}
[Serializable]
public class TargetObjective
{
    public TargetObjective(uint count, ItemTypes name)
    {
        this.count = count;
        this.itemType = name;
    }
    [SerializeField] private uint count;
    [SerializeField] private ItemTypes itemType;
    
    public string name => itemType.ToString();
}
[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField] private int levelCount;
    [SerializeField] private int totalMove;
    [SerializeField] private Vector2Int size;
    [SerializeField] private TargetObjective[] targetObjectives;
    // Private set Public get 
    public int LevelCount
    {
        get => levelCount;
        set => levelCount = value;
    }
    public int TotalMove => totalMove;
    public Vector2Int Size => size;
}
