using UnityEngine;

namespace _InputTest.Entity.Scripts.Combat.Monobehaviours
{
    public class DestructibleDestroyObject : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private float destroyAfter;
    
        public void OnDestroyed(GameObject destroyer)
        {
            Destroy(gameObject, destroyAfter);
        }
    }
}
