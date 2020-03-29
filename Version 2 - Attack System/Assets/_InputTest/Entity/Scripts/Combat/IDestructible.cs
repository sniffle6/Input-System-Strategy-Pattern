using UnityEngine;

namespace _InputTest.Entity.Scripts.Combat
{
    public interface IDestructible
    {
        void OnDestroyed(GameObject destroyer);
    }
}
