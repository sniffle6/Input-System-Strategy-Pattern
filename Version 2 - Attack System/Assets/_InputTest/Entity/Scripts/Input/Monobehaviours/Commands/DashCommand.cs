using System.Collections;
using UnityEngine;

namespace _InputTest.Entity.Scripts.Input.Monobehaviours.Commands
{
    [RequireComponent(typeof(InputMoveCommand))]
    public class DashCommand : Command
    {
        public float speed;
        
        private IMoveInput _move;
        private InputMoveCommand _moveCommand;
        private Animator _animator;
        private bool _active;
        private static readonly int Dashing = Animator.StringToHash("Dashing");

        private void Start()
        {
            _moveCommand = GetComponent<InputMoveCommand>();
            _move = GetComponent<IMoveInput>();
            _animator = GetComponent<Animator>();
        }

        public override void Execute()
        {
            _active = true;
            _moveCommand.speedModifier = speed;
            _animator.SetBool(Dashing, true);
            _animator.speed = speed / 3;
            StartCoroutine(OnUpdate());
        }

        private IEnumerator OnUpdate()
        {
            while (_active)
            {
                if (_move.MoveDirection == Vector3.zero)
                    _animator.SetBool(Dashing, false);
                else
                {
                    if(_animator.GetBool(Dashing) == false)
                        _animator.SetBool(Dashing, true);
                }
                yield return null;
            }
        }
        
        public override void Complete()
        {
            _active = false;
            StopAllCoroutines();
            _moveCommand.speedModifier = 1;
            _animator.speed = 1;
            _animator.SetBool(Dashing, false);
        }

    }
}
