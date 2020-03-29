using UnityEngine;

namespace _InputTest.Entity.Scripts.Input.Monobehaviours.Commands
{
    public abstract class Command : MonoBehaviour
    {
        
        public virtual void Execute()
        {
        }
        public virtual void Complete()
        {
            
        }
    }
}
