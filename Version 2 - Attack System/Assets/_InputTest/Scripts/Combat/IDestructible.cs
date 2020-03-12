using UnityEngine;

namespace _InputTest.Scripts.Combat
{
    public interface IDestructible
    {
        void OnDestroyed(GameObject destroyer);
    }
}
