using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LineManager : Instancable<LineRenderer>
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private float lineSegmentLenght = 0.1f;
    [SerializeField] private Sprite _selectedTileBackground;
    public static List<GameObject> _selectedObjects = new List<GameObject>();
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            if(raycastResults.Count > 0)
            {
                foreach(var result in raycastResults)
                {
                    if (result.gameObject.CompareTag("Food"))
                    {
                        GameObject obj = result.gameObject;
                        if (_selectedObjects.Count == 0 || !_selectedObjects.Contains(obj))
                        {
                            
                            _selectedObjects.Add(obj);
                            obj.GetComponent<Image>().sprite = _selectedTileBackground;
                            line.positionCount = _selectedObjects.Count;
                            line.SetPosition(_selectedObjects.Count - 1, obj.transform.position);
                        }
                        
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedObjects.Count >= 3)
            {
                var isSameType = true;
                for (int i = 0; i < _selectedObjects.Count - 1; i++)
                {
                    if (_selectedObjects[i].GetComponent<TileManager>()._item !=
                        _selectedObjects[i + 1].GetComponent<TileManager>()._item)
                    {
                        isSameType = false;
                        break;
                    }
                   
                }
                if (isSameType)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    GridManager.instance.FindDestroyObjects();
                  
                }
                else
                {
                      print("Unsuccess");         
                }
            }
            line.positionCount = 0;
            _selectedObjects.Clear();
        }
    }
}