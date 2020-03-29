using System.Collections;
using _InputTest.Entity.Scripts.Combat;
using UnityEngine;

namespace _InputTest.Entity.Scripts.Input.Monobehaviours.Commands
{
    public class AttackCommand : Command
    {
        public bool isWeaponDrawn;

        [SerializeField] private LayerMask layerToAttack;
        [SerializeField] private float hitBoxOffset;
        [SerializeField] private Vector3 hitBoxSize;
        [SerializeField] private float attackAfterSeconds;
        [SerializeField] private Command drawCommand;

        private Animator _animator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private const string Punch = "Punch";
        private Transform _transform;
        private WaitForSeconds _wait;
        private Coroutine _attackCoroutine;

        private readonly Collider[] _buffer = new Collider[10];

        private void Start() => (_transform, _animator, _wait) =
            (transform, GetComponent<Animator>(), new WaitForSeconds(attackAfterSeconds));

        private void OnValidate() => _wait = new WaitForSeconds(attackAfterSeconds);

        public override void Execute()
        {
            if (isWeaponDrawn == false)
            {
                if (drawCommand)
                    drawCommand.Execute();
                return;
            }

            if (_animator.GetCurrentAnimatorStateInfo(1).IsName(Punch))
                return;
            _animator.SetTrigger(Attack);
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
            _attackCoroutine = StartCoroutine(Attacking());
        }

        private IEnumerator Attacking()
        {
            yield return _wait;
            var size = Physics.OverlapBoxNonAlloc(
                (_transform.position + (_transform.forward / hitBoxOffset) + Vector3.up), hitBoxSize / 2, _buffer,
                _transform.rotation, layerToAttack);
            var i = 0;
            while (i < size)
            {
                var attackables = _buffer[i].GetComponents<IAttackable>();
                foreach (var attackable in attackables)
                {
                    attackable.OnAttacked(gameObject, new Attack(10, false));
                }

                i++;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((transform.position + (transform.forward / hitBoxOffset) + Vector3.up), hitBoxSize);
        }
    }
}