using UnityEngine;

namespace _InputTest.Entity.Scripts.Combat.Monobehaviours
{
    public class DestructiblePlayAnimation : MonoBehaviour, IDestructible
    {
        [SerializeField]
        private string stateName = "Dying";
        [SerializeField]
        private string triggerName = "Death";
        
        
        private Animator _animator;
        private static int _death;

        private void Awake() => (_death, _animator) = (Animator.StringToHash(triggerName), GetComponent<Animator>());


        public void OnDestroyed(GameObject destroyer)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
                return;
            _animator.SetTrigger(_death);
        }
    }
}
