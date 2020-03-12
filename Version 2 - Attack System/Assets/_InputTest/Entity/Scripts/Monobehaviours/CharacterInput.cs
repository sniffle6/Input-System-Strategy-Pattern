using System;
using System.Collections.Generic;
using System.Linq;
using _InputTest.Scripts;
using _InputTest.Scripts.Combat;
using _InputTest.Scripts.Combat.Monobehaviours;
using _InputTest.Scripts.Input;
using _InputTest.Scripts.Input.Monobehaviours.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _InputTest.Entity.Scripts.Monobehaviours
{
    public class CharacterInput : MonoBehaviour, IInteractInput, IMoveInput, IRotationInput, ISkillInput, IAttackInput
    {
        [Header("Input Commands")] public Command interactInput;
        public Command movementInput;
        public Command analogRotationInput;
        public Command mouseRotationInput;
        public Command skillInput;
        public Command attackInput;


        [Header("Input Values")] [Space(20)] [SerializeField]
        private Vector3 rotationDirection;

        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private bool isPressingInteract;
        [SerializeField] private bool isUsingSkill;
        [SerializeField] private bool isAttacking;


        public Vector3 MoveDirection => moveDirection;
        public Vector3 RotationDirection
        {
            get => rotationDirection;
            set => rotationDirection = value;
        }
        public bool IsUsingSkill => isUsingSkill;
        public bool IsPressingInteract => isPressingInteract;
        public bool IsAttacking => isAttacking;

        
        private PlayerInputActions _inputActions;
        private const string LeftMouseButton = "Left Button";

        private (string messageForWorld, bool showMessageToWorld) myTuple = ("Hello World", true);

        private void Awake()
        {
            _inputActions = new PlayerInputActions();


            int x = 12;
            int y = 4;
            var playerPosition = (x, y);
            
            print($"Player is at X:{playerPosition.x}, Y:{playerPosition.y}");
            
            var item = (Id: 1, ItemName: "Sword", Damage: 32);
            
            if (myTuple.showMessageToWorld == true)
            {
                print($"My first Tuple says: {myTuple.messageForWorld}");
            }
        }



        private void OnEnable()
        {
            _inputActions.Enable();

            if (movementInput)
                _inputActions.Player.Movement.performed += OnMoveInput;

            if (analogRotationInput)
                _inputActions.Player.AnalogAim.performed += OnAnalogAimInput;

            if (interactInput)
            {
                _inputActions.Player.Interact.started += OnInteractStart;
                _inputActions.Player.Interact.canceled += OnInteractEnded;
            }

            if (skillInput)
            {
                _inputActions.Player.Skill.performed += OnSkillUse;
                _inputActions.Player.Skill.canceled += OnSkillEnd;
            }

            if (attackInput)
            {
                _inputActions.Player.Attack.performed += OnAttackStart;
                _inputActions.Player.Attack.canceled += OnAttackEnd;
            }
        }





        private void OnMoveInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            moveDirection = new Vector3(value.x, 0, value.y);
            if (movementInput != null)
                movementInput.Execute();
        }
        
        private void OnAnalogAimInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            rotationDirection = new Vector3(value.x, 0, value.y);
            if (analogRotationInput != null)
                analogRotationInput.Execute();
        }
        
        private void OnInteractStart(InputAction.CallbackContext context)
        {
            if (context.control.displayName != LeftMouseButton) return;
            isPressingInteract = true;
            mouseRotationInput.Execute();
        }
        
        private void OnInteractEnded(InputAction.CallbackContext context)
        {
            if (interactInput != null)
                interactInput.Execute();

            isPressingInteract = false;

            if (context.action.activeControl.device != Mouse.current) return;
            rotationDirection = Vector3.zero;
        }
        
        private void OnSkillUse(InputAction.CallbackContext context)
        {
            isUsingSkill = true;
            if (skillInput != null)
                skillInput.Execute();
        }

        private void OnSkillEnd(InputAction.CallbackContext context)
        {
            isUsingSkill = false;
            if (skillInput != null)
                skillInput.Complete();
        }
        
        private void OnAttackStart(InputAction.CallbackContext context)
        {
            isAttacking = true;
            if (attackInput)
                attackInput?.Execute();
        }

        private void OnAttackEnd(InputAction.CallbackContext context)
        {
            isAttacking = false;
            if (attackInput)
                attackInput.Complete();
        }


        private void OnDisable()
        {
            _inputActions.Player.Movement.performed -= OnMoveInput;
            _inputActions.Player.Interact.started -= OnInteractStart;
            _inputActions.Player.Interact.canceled -= OnInteractEnded;
            _inputActions.Player.AnalogAim.performed -= OnAnalogAimInput;
            _inputActions.Player.Skill.performed -= OnSkillUse;

            _inputActions.Disable();
        }
    }
}