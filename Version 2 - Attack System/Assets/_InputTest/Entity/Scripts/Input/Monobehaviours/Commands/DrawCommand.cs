using UnityEngine;

namespace _InputTest.Entity.Scripts.Input.Monobehaviours.Commands
{
    public class DrawCommand : Command
    {
        [SerializeField] private DrawState state = DrawState.Idle;
        [SerializeField] private Transform weaponModel;
        [SerializeField] private Transform handPosition;
        [SerializeField] private Transform hipPosition;

        private AttackCommand _owner;
        private Animator _animator;
        private readonly int _sheathWeapon = Animator.StringToHash("SheathWeapon");
        private readonly int _drawWeapon = Animator.StringToHash("DrawWeapon");
        private readonly int _weaponStance = Animator.StringToHash("WeaponStance");
        private readonly int _handStance = Animator.StringToHash("HandStance");

        private void Awake()
        {
            _owner = GetComponent<AttackCommand>();
            _animator = GetComponent<Animator>();
        }

        public override void Execute()
        {
            switch (state)
            {
                case DrawState.Idle:
                    state = DrawState.DrawingWeapon;
                    _animator.SetTrigger(_owner.isWeaponDrawn ?  _sheathWeapon : _drawWeapon);
                    break;
            }
        }


        //controlled by animator
        private void OnDrawSwordOneComplete()
        {
            weaponModel.SetParent(handPosition);
            weaponModel.localPosition = Vector3.zero;
            weaponModel.localRotation = Quaternion.identity;
            _owner.isWeaponDrawn = true;
            _animator.SetTrigger(_weaponStance);
            state = DrawState.Idle;
        }
        
        
        //controlled by animator
        private void OnSheathWeaponComplete()
        {
            weaponModel.SetParent(hipPosition);
            weaponModel.localPosition = Vector3.zero;
            weaponModel.localRotation = Quaternion.identity;
            _owner.isWeaponDrawn = false;
            _animator.SetTrigger(_handStance);
            state = DrawState.Idle;
        }
        
    }

    public enum DrawState
    {
        Idle,
        DrawingWeapon,
        SheathingWeapon
    }
}