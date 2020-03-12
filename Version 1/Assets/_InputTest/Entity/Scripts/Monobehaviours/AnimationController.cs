using _InputTest.Scripts;
using UnityEngine;

namespace _InputTest.Entity.Scripts.Monobehaviours
{
    public class AnimationController : MonoBehaviour
    {
        private IMoveInput _input;
        private Animator _animator;
        private static readonly int HorizontalAnimKey = Animator.StringToHash("Horizontal");
        private static readonly int VerticalAnimKey = Animator.StringToHash("Vertical");

        private Transform _transform;

        private void Awake()
        {
            _input = GetComponent<IMoveInput>();
            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var verticalDot = Vector3.Dot(_transform.forward, _input.MoveDirection);
            var horizontalDot = Vector3.Dot(_transform.right,_input.MoveDirection);
            _animator.SetFloat(HorizontalAnimKey, horizontalDot);
            _animator.SetFloat(VerticalAnimKey, verticalDot);
        }
    }
}