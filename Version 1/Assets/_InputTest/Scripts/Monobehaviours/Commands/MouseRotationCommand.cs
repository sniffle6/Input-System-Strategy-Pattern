using System.Collections;
using UnityEngine;

namespace _InputTest.Scripts.Monobehaviours.Commands
{
    public class MouseRotationCommand : Command
    {
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
            while (_interact.IsPressingInteract)
            {
                var screenSize = new Vector3(Screen.width, 0, Screen.height) / 2;
                _rigidbody.rotation = Quaternion.LookRotation((_rotate.RotationDirection - screenSize).normalized);
                yield return null;
            }

            _rotate.RotationDirection = Vector3.zero;


            _coroutine = null;
        }
    }
}