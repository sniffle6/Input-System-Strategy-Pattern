using System.Collections;
using UnityEngine;

namespace _InputTest.Scripts.Monobehaviours.Commands
{
    public class InputMoveCommand : Command
    {
        public float turnSmoothing;
        public AnimationCurve speed;

        private Rigidbody _rigidbody;
        private IMoveInput _move;
        private IRotationInput _rotate;
        private Coroutine _moveCoroutine;
        private Coroutine _rotateCoroutine;
        private Transform _transform;

        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _move = GetComponent<IMoveInput>();
            _rotate = GetComponent<IRotationInput>();
            _transform = transform;
        }

        public override void Execute()
        {
            if (_moveCoroutine == null)
                _moveCoroutine = StartCoroutine(Move());
            if (_rotateCoroutine == null)
                _rotateCoroutine = StartCoroutine(Rotate());
        }

        private IEnumerator Move()
        {
            while (_move.MoveDirection != Vector3.zero)
            {
                var time = (Time.fixedDeltaTime * speed.Evaluate(_move.MoveDirection.magnitude));
                
                _rigidbody.MovePosition(_transform.position + _move.MoveDirection * time);
                
                yield return null;
            }

            _moveCoroutine = null;
        }

        private IEnumerator Rotate()
        {
            var time = 0.0f;

            while (_move.MoveDirection != Vector3.zero)
            {
                yield return new WaitUntil(() => (_rotate.RotationDirection == Vector3.zero));

                if (_move.MoveDirection == Vector3.zero) continue;

                if (_move.MoveDirection.magnitude <= 0.5f)
                {
                    var targetRotation = Quaternion.LookRotation(_move.MoveDirection, Vector3.up);
                    time += Time.fixedDeltaTime * turnSmoothing * _move.MoveDirection.magnitude;
                    var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, time);
                    _rigidbody.MoveRotation(newRotation);
                }
                else
                {
                    _rigidbody.MoveRotation(Quaternion.LookRotation(_move.MoveDirection, Vector3.up));
                }
            }

            _rotateCoroutine = null;
        }
    }
}