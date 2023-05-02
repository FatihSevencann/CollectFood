using System;
using System.Collections;
using System.Collections.Generic;
using Level;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : Instancable<Spawner>
{
    public List<GameObject> ObjectstoSpawn = new List<GameObject>();
    private int _gridX, _girdY;
    [SerializeField] private float _gravity, spawnerYOfsett;
    private Vector2 _groundForSpawner;
    private bool _move = false;

    [SerializeField] private Rigidbody2D _rigidbody;
    private void Start()
    {
        var position = transform.position;
        _gridX = (int)transform.position.y;
        _girdY = (int)transform.position.x;
        _groundForSpawner = transform.position + Vector3.up * spawnerYOfsett;
        position = _groundForSpawner;
        transform.position = position;
    }
    public void AddToSpawnList(GameObject tile) => ObjectstoSpawn.Add(tile);
    public void StartSpawning()
    {
        int tileAmount = ObjectstoSpawn.Count;
        if (tileAmount > 0)
        {
            IncreaseSpawn(tileAmount);
            for (int i = 0; i < tileAmount; i++)
            {
                GameObject objects = ObjectstoSpawn[i];
                objects.SetActive(true);
                objects.GetComponent<TileManager>().ReuseTile(transform.position + new Vector3(0, i - tileAmount), new Vector2Int(_gridX, _girdY + i - tileAmount));
            }
            ObjectstoSpawn = new List<GameObject>();
        }
    }
    public void IncreaseSpawn(int tile)
    {
        transform.position += Vector3.up * (tile);
        _move = true; _rigidbody.constraints=RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
    private void FixedUpdate()
    {
        if (_move)
        {
            if (transform.position.y <= _groundForSpawner.y)
            {
                 _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
                _move = false;
                 _rigidbody.MovePosition(_groundForSpawner);
            }
        }
    }
}