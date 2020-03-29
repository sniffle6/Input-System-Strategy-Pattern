using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaGenerator : MonoBehaviour
{
    [SerializeField] private Vector3 center;
    [SerializeField] private GameObject[] arenaFences;
    [SerializeField] private float radius;
    [SerializeField] private float objectSpacing;

    private List<GameObject> _spawnedFences = new List<GameObject>();
    
    [ContextMenu("Generate Arena")]
    public void GenerateArena()
    {
        if(arenaFences.Length <= 0)
            return;
        DestroyArena();   
        var numObjects = 360 / objectSpacing;
        var angle = 0f;
        for (int i = 0; i < numObjects; i++)
        {
            var pos = new Vector3(center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad),0, center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad));
            var fence = Instantiate(arenaFences[Random.Range(0, arenaFences.Length)], transform, true);
            fence.transform.position = pos;
            fence.transform.LookAt(center);
            _spawnedFences.Add(fence);

            angle += objectSpacing;
        }
    }

    [ContextMenu("Destroy Arena")]
    public void DestroyArena()
    {
        foreach (var t in _spawnedFences)
        {
            DestroyImmediate(t);
        }

        _spawnedFences.Clear();
    }

    
}
