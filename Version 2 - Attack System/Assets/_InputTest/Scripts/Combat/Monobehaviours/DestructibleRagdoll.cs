using System;
using UnityEngine;

namespace _InputTest.Scripts.Combat.Monobehaviours
{
    public class DestructibleRagdoll : MonoBehaviour, IDestructible
    {
        [SerializeField] private GameObject ragdollPrefab;
        [SerializeField] private float force;

        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
        }

        public void OnDestroyed(GameObject destroyer)
        {
            var position = _transform.position;
            var ragdoll = Instantiate(ragdollPrefab, position, transform.rotation);
            var rb = ragdoll.GetComponentInChildren<Rigidbody>();
            rb.AddForce((position - destroyer.transform.position) * force, ForceMode.Impulse);
        }
    }
}
