using UnityEngine;

namespace _InputTest.Entity.Scripts.Combat.Monobehaviours
{
    public class AttackableTakeDamage : MonoBehaviour, IAttackable
    {
        private IHealth _health;
        private IDestructible[] _destructibles;
        
        private void Awake() => (_destructibles, _health) = (GetComponents<IDestructible>(), GetComponent<IHealth>());


        public void OnAttacked(GameObject attacker, Attack attack)
        {
            if (_health != null)
            {
                _health.AdjustHeath(attacker, -attack.Damage);
            }
            else
            {
                foreach (var destructible in _destructibles)
                {
                    destructible.OnDestroyed(attacker);
                }
            }

        }
    }
}
