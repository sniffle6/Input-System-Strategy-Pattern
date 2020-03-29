using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _InputTest.Entity.Scripts.Monobehaviours
{
    public class EntitySpawner : MonoBehaviour
    {
        [SerializeField] private bool spawning;
        [Space(20)]
        [Header("Spawn Data")] [SerializeField]
        private GameObject entityPrefab;

        [SerializeField] private float spawnDistance;
        [SerializeField] private int entitiesToSpawn;
        [SerializeField] private float spawnSpeed;

        [Header("Entity Data")] [Space(20)] [Range(0.1f, 1)]
        [SerializeField] private float entitySpeed;
        [SerializeField] private Transform target;

        private Vector3 _spawnVector;

        private void Start()
        {
            _spawnVector = new Vector3(spawnDistance, 0, spawnDistance);
            spawning = true;
            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            while (spawning)
            {
                for (int i = 0; i < entitiesToSpawn; i++)
                {
                    var angle = Random.Range(0f, 360f);
                    var e = Instantiate(entityPrefab).transform;
                    e.SetParent(transform);
                    e.Rotate(new Vector3(0, angle, 0));
                    e.Translate(_spawnVector);
                    var ai = e.GetComponent<AiInput>();
                    ai.Initiate(target, entitySpeed);
                }

                yield return new WaitForSeconds(spawnSpeed);
            }
        }
    }
}