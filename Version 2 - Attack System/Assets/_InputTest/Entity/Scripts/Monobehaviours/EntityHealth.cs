using System;
using System.Collections.Generic;
using _InputTest.Scripts.Combat;
using JetBrains.Annotations;
using UnityEngine;

namespace _InputTest.Entity.Scripts.Monobehaviours
{
    public class EntityHealth : MonoBehaviour, IHealth, ILive
    {
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private bool isAlive;

        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealth;
        public bool IsAlive => isAlive;


        [SerializeField] private IDestructible[] destructibles;

        private void Awake() => (destructibles, isAlive) = (GetComponents<IDestructible>(), currentHealth > 0);


        public void AdjustHeath(GameObject adjuster, int value)
        {
            if (currentHealth == 0)
                return;

            currentHealth += value;
            CheckForDeath(adjuster);
        }

        private void CheckForDeath(GameObject killer)
        {
            if (currentHealth > 0)
                return;
            isAlive = false;
            foreach (var d in destructibles)
            {
                d.OnDestroyed(killer);
            }
        }
    }
}