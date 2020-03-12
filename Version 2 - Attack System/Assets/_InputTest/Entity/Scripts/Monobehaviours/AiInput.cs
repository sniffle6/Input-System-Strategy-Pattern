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
        public Command moveCommand;

        public Vector3 MoveDirection { get; private set; }
        public Vector3 RotationDirection { get; set; }

        private ILive _live;

        private void Awake()
        {
            _live = GetComponent<ILive>();
        }

        private void Start()
        {
            MoveDirection = new Vector3(0, 0, -0.25f);
            moveCommand.Execute();

            InvokeRepeating(nameof(ChangeDirection), 1, Random.Range(1, 4));
        }

        private void ChangeDirection()
        {
            if (!_live.IsAlive)
            {
                MoveDirection = Vector3.zero;
            }
            else
            {
                MoveDirection = new Vector3((Random.Range(-1, 2) / 4.0f), 0, z: (Random.Range(-1, 2) / 4.0f));
            }

            moveCommand.Execute();
        }
    }
}