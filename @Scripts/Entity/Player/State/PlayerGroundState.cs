using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Objects;
using BIS.Pool;
using System;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerGroundState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        private Camera _mainCamera;
        private float _angle;
        public PlayerGroundState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
            _mainCamera = Camera.main;
        }

        public override void Enter()
        {
            base.Enter();
            //_player.InputSO.LeftClickAttackEvent += HandleAttackEvent;
        }
        //private void HandleAttackEvent()
        //{
        //    Vector2 worldMousePos = _mainCamera.ScreenToWorldPoint(_player.InputSO.MousePos);

        //    GameObject bullet = PoolManager.SpawnFromPool("PlayerBullet", _player.transform.position);

        //    Vector2 direction = (worldMousePos - (Vector2)_entity.transform.position).normalized;

        //    bullet.GetComponent<Bullet>().SetMovement(direction, _player.Stat.bulletSpeed.GetValue());
        //}

        public override void Exit()
        {
            //_player.InputSO.LeftClickAttackEvent -= HandleAttackEvent;

            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            Vector2 worldMousePos = _mainCamera.ScreenToWorldPoint(_player.InputSO.MousePos);

            Vector2 directionToMouse = worldMousePos - (Vector2)_entity.transform.position;
            _angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            _entity.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
        }
    }
}
