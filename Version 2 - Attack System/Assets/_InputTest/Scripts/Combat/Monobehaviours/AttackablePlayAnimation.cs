using _InputTest.Entity.Scripts;
using UnityEngine;

namespace _InputTest.Scripts.Combat.Monobehaviours
{
    public class AttackablePlayAnimation : MonoBehaviour, IAttackable
    {
        [SerializeField] private string triggerName = "Hit";

        private int _hit;
        private Animator _animator;
        private ILive _life;
        
        private const string StateToPlayFrom = "Empty State";

        private void Awake()
        {
            _life = GetComponent<ILive>();
            _animator = GetComponent<Animator>();
            _hit = Animator.StringToHash(triggerName);
        }

        public void OnAttacked(GameObject attacker, Attack attack)
        {
            if (_life != null && _life.IsAlive)
            {
                if (_animator.GetCurrentAnimatorStateInfo(1).IsName(StateToPlayFrom))
                    _animator.SetTrigger(_hit);
            }
            else if (_life == null)
            {
                    _animator.SetTrigger(_hit);
            }
        }
    }
}