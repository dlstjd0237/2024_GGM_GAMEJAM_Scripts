using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using BIS.Objects;
using BIS.Pool;
using DG.Tweening;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyBiterrainAttackState : EntityState
    {
        private EnemyBiterrain _enemy;

        public EnemyBiterrainAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as EnemyBiterrain;
        }
        public override void Enter()
        {
            base.Enter();
            _enemy.GetCompo<EntityMover>().StopImmediately();
            Sequence seq = DOTween.Sequence();
            seq.Append(_enemy.bottom.DOLocalMoveY(-1f, 2f));
            seq.JoinCallback(() => PoolManager.SpawnFromPool("BossOrb", _enemy.attackTrm.position).GetComponent<Bossorb>().PlayOrb(_enemy.Stat.attackDamage.GetValue()));
            seq.AppendInterval(1);
            seq.Append(_enemy.bottom.DOLocalMoveY(-0.1f, 0.05f));
            seq.JoinCallback(() => Manager.Camera.ShakeCamera(Vector3.one * 3f, 5f, 5f, 2f));
            seq.AppendInterval(2);
            seq.AppendCallback(() => _enemy.ChangeState("IDLE"));
        }



    }
}
