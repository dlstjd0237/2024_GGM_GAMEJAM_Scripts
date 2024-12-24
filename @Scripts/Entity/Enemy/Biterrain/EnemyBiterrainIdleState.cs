using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
using DG.Tweening;
using BIS.Enemys;
using BIS.Managers;

namespace BIS.Enemys
{
    public class EnemyBiterrainIdleState : EntityState
    {
        private EnemyBiterrain _enemy;
        private EntityMover _mover;
        public EnemyBiterrainIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as EnemyBiterrain;
            _mover = _enemy.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.bottom.DOLocalMoveY(-0.15f, 1).SetLoops(-1, LoopType.Yoyo);
            DOVirtual.DelayedCall(_enemy.Stat.attackCool.GetValue(), () => _enemy.ChangeState("ATTACK"));
        }

        public override void Update()
        {
            base.Update();
            Transform targetPos = Manager.GameScene.Player.transform;
            Vector2 playerDirection = (targetPos.position - _enemy.transform.position).normalized;
            _mover.SetMovement(playerDirection);
        }

        public override void Exit()
        {
            _enemy.bottom.DOKill();
            base.Exit();
        }
    }
}
