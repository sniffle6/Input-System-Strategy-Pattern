using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _InputTest.Entity.Scripts.Input.Monobehaviours.Commands
{
    public class MouseRotationCommand : Command
    {
        
        public float turnSmoothing;
        
        private IRotationInput _rotate;
        private IInteractInput _interact;
        private Rigidbody _rigidbody;
        private Coroutine _coroutine;

        private void Awake()
        {
            _rotate = GetComponent<IRotationInput>();
            _interact = GetComponent<IInteractInput>();
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
            while (_interact.IsPressingInteract)
            {
                var screenSize = new Vector3(Screen.width, 0, Screen.height) / 2;
                var mousePosition = new Vector3(Mouse.current.position.ReadValue().x, 0,
                    Mouse.current.position.ReadValue().y);

                
                
                _rotate.RotationDirection = (mousePosition - screenSize).normalized;
                
                
                if (turnSmoothing > 0)
                {
                    var targetRotation = Quaternion.LookRotation(_rotate.RotationDirection, Vector3.up);
                    time += Time.fixedDeltaTime * turnSmoothing * _rotate.RotationDirection.magnitude;
                    var newRotation = Quaternion.Lerp(_rigidbody.rotation, targetRotation, time);

                    _rigidbody.MoveRotation(newRotation);
                }
                else
                {
                    _rigidbody.MoveRotation(Quaternion.LookRotation(_rotate.RotationDirection, Vector3.up));
                }

                yield return null;
            }

            _coroutine = null;
        }
    }
}