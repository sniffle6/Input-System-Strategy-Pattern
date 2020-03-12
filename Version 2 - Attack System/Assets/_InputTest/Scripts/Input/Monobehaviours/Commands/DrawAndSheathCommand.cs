using System;
using System.Diagnostics;
using UnityEngine;

namespace _InputTest.Scripts.Input.Monobehaviours.Commands
{
    public class DrawAndSheathCommand : Command
    {
        public bool isWeaponDrawn;

        [SerializeField] private DrawState state = DrawState.NotDoing;
        [SerializeField] private Transform weaponModel;
        [SerializeField] private Transform handPosition;
        [SerializeField] private Transform hipPosition;

        private Animator _animator;
        private readonly int _drawWeapon = Animator.StringToHash("DrawWeapon");
        private readonly int _sheathWeapon = Animator.StringToHash("SheathWeapon");
        private readonly int _weaponStance = Animator.StringToHash("WeaponStance");
        private readonly int _handStance = Animator.StringToHash("HandStance");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Execute()
        {
            switch (state)
            {
                case DrawState.NotDoing:
                    state = DrawState.Doing;
                    _animator.SetTrigger(isWeaponDrawn ? _sheathWeapon : _drawWeapon);
                    break;
            }
        }

          
        
        private void OnDrawSwordOneComplete()
        {
            weaponModel.SetParent(handPosition);
            weaponModel.localPosition = Vector3.zero;
            weaponModel.localRotation = Quaternion.identity;
            isWeaponDrawn = true;
            _animator.SetTrigger(_weaponStance);
            state = DrawState.NotDoing;

            TryGetComponent<Rigidbody>(out _);


        }

        private void OnSheathWeaponComplete()
        {
            weaponModel.SetParent(hipPosition);
            weaponModel.localPosition = Vector3.zero;
            weaponModel.localRotation = Quaternion.identity;
            isWeaponDrawn = false;
            _animator.SetTrigger(_handStance);
            state = DrawState.NotDoing;
        }
    }

    enum DrawState
    {
        NotDoing,
        Doing,
    }
}