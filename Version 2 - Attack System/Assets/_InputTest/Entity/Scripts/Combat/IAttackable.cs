using UnityEngine;

namespace _InputTest.Entity.Scripts.Combat
{
    public interface IAttackable
    {
        void OnAttacked(GameObject attacker, Attack attack);
    }
}
