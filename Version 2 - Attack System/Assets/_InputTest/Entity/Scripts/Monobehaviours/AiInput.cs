using System;
using _InputTest.Scripts;
using _InputTest.Scripts.Combat;
using _InputTest.Scripts.Input;
using _InputTest.Scripts.Input.Monobehaviours.Commands;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _InputTest.Entity.Scripts.Monobehaviours
{
    public class AiInput : MonoBehaviour, IMoveInput, IRotationInput
    {
        [SerializeField] public Transform target;
        [SerializeField] private Command moveCommand;
        [SerializeField] private Command attackCommand;
        [SerializeField] private Command drawSheathCommand;

        private float _speed;
        private ILive _live;

        public Vector3 MoveDirection { get; private set; }
        public Vector3 RotationDirection { get; set; }

        private void Awake()
        {
            _live = GetComponent<ILive>();
        }

        private void Start()
        {
            if (drawSheathCommand)
                drawSheathCommand.Execute();
        }

        public void Initiate(Transform moveTo, float speed)
        {
            target = moveTo;
            _speed = speed;
            MoveDirection = (target.position - transform.position).normalized * speed;
            moveCommand.Execute();
        }


        private void Update()
        {
            if (!target || !_live.IsAlive)
            {
                MoveDirection = Vector3.zero;
                attackCommand.StopAllCoroutines();
                return;
            }

            if (Vector3.Distance(target.position, transform.position) <= 1.25f)
            {
                if (attackCommand)
                    attackCommand.Execute();
                MoveDirection = Vector3.zero;
            }
            else
                MoveDirection = (target.position - transform.position).normalized * _speed;


            moveCommand.Execute();
        }
    }
}