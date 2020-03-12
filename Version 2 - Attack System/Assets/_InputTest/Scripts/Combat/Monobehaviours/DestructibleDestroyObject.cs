using System.Collections;
using System.Collections.Generic;
using _InputTest.Scripts.Combat;
using UnityEngine;

public class DestructibleDestroyObject : MonoBehaviour, IDestructible
{
    [SerializeField]
    private float destroyAfter;
    
    public void OnDestroyed(GameObject destroyer)
    {
        Destroy(gameObject, destroyAfter);
    }
}
