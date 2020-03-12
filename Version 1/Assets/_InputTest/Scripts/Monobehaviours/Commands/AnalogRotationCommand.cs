using System.Collections;
using UnityEngine;

namespace _InputTest.Scripts.Monobehaviours.Commands
{
    public class AnalogRotationCommand : Command
    {
        public float turnSmoothing;

        private IRotationInput _input;
        private Rigidbody _rigidbody;
        private Coroutine _coroutine;

        private void Awake()  
        {
             _input = GetComponent<IRotationInput>();
            _rigidbody = GetComponent<Rigidbody>();
        }


        public override void Execute()
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(Rotate());
        }

        private IEnumerator Rotate()
        {
            var time = 0.0f;
            while (_input.RotationDirection != Vector3.zero)
            {
                if (_input.RotationDirection.magnitude <= 0.5f)
                {
                    var targetRotation = Quaternion.LookRotation(_input.RotationDirection, Vector3.up);
                    time += Time.deltaTime * turnSmoothing * _input.RotationDirection.magnitude;
                    var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, time);

                    _rigidbody.MoveRotation(newRotation);
                }
                else
                {
                    _rigidbody.MoveRotation(Quaternion.LookRotation(_input.RotationDirection, Vector3.up));
                }

                yield return null;
            }

            _coroutine = null;
        }
    }
}