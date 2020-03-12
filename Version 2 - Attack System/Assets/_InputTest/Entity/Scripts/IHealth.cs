using UnityEngine;

namespace _InputTest.Entity.Scripts
{
    public interface IHealth 
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void AdjustHeath(GameObject adjuster, int value);
    }
}
