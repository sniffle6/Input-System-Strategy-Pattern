using UnityEngine;

namespace _InputTest.Scripts.Combat
{
    public interface IAttackable
    {
        void OnAttacked(GameObject attacker, Attack attack);
    }
}
