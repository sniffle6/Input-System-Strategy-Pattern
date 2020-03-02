using UnityEngine;

namespace _InputTest.Scripts.Monobehaviours.Commands
{
    public class DashCommand : Command
    {
        public float dashForce;
        public float dashLength;
        
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Animator _animator;
        private static readonly int Dashing = Animator.StringToHash("Dashing");

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _animator = GetComponent<Animator>();
        }

        public override void Execute()
        {
            _animator.SetBool(Dashing, true);
            _rigidbody.AddForce(_transform.forward*dashForce, ForceMode.VelocityChange);
            _animator.SetBool(Dashing, true);
            Invoke(nameof(SetAnimFalse), dashLength);
        }

        private void SetAnimFalse()
        {
            _animator.SetBool(Dashing, false);
        }
    }
}
