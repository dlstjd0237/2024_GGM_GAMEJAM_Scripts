using UnityEngine;
using BIS.Init;
using static BIS.Utility.Util;
using System;
using DG.Tweening;
using BIS.Core;
using BIS.Stats;

namespace BIS.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponentInit, IAfterInit
    {

        private Entity _enttiy;

        private Rigidbody2D _rig2D;
        private EntityRenderer _renderer;


        [Space]
        [Header("Info")]
        private CharactersStat _stat;
        [SerializeField] private float _moveSpeedMultiplier = 1;
        private Vector2 _movementVelocity;

        public event Action<Vector2> OnMovement;

        [field: SerializeField] public bool CanManualMove { get; set; } = true;

        public void Initalize(Entity entity)
        {
            _enttiy = entity;
            _rig2D = entity.GetComponent<Rigidbody2D>();
            _renderer = entity.GetCompo<EntityRenderer>();
            _stat = entity.Stat;

        }
        public void AfterInit() //만약 스텟 시스템을 넣는다면 이곳에 들어갈 예정
        {
        }


        //private void HandleMoveSpeedChange( float current, float prev) => _moveSpeed = current;


        private void FixedUpdate()
        {
            if (CanManualMove)
                _rig2D.linearVelocity = _movementVelocity * _stat.moveSpeed.GetValue() * _moveSpeedMultiplier;
            OnMovement?.Invoke(_rig2D.linearVelocity);
        }

        public void SetMovement(Vector2 dir)
        {
            _movementVelocity = dir;
        }

        public void StopImmediately()
        {
            _rig2D.linearVelocity = Vector2.zero;
            _movementVelocity = Vector2.zero;
        }

        public void SetMovementMultiplier(float value) => _moveSpeedMultiplier = value;
        public void SetGravityMultiplier(float value) => _rig2D.gravityScale = value;

        public void AddForceToEntity(Vector2 force, ForceMode2D mode = ForceMode2D.Impulse)
        {
            _rig2D.AddForce(force, mode);
        }

        public void DashMovement(Vector2 dir, float speed)
        {
            _movementVelocity = dir * speed;
        }

        #region KnockBack

        public void KnockBack(Vector2 force, float time)
        {
            CanManualMove = false;
            StopImmediately();
            AddForceToEntity(force);
            DOVirtual.DelayedCall(time, () => CanManualMove = true);
        }

        #endregion

    }
}
