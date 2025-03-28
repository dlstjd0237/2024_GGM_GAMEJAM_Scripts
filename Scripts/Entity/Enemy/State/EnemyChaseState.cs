using BIS.Animators;
using BIS.Entities;
using BIS.Managers;
using UnityEngine;
using System.Collections;
using BIS.Pool;
using BIS.Objects;

namespace BIS.Enemys
{
    public class EnemyChaseState : EnemyGroundState
    {
        private Transform targetTrm;
        public EnemyChaseState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.StartCoroutine(FireBullet());
            _mover.StopImmediately();
        }
        public override void Update()
        {
            base.Update();

            targetTrm = Manager.Game.FindToTarget(_enemy.transform, _radius, _whatIsPlayer);
            if (targetTrm == null)
            {
                _enemy.ChangeState("IDLE");
            }

        }

        public override void Exit()
        {
            _enemy.StopCoroutine(FireBullet());
            base.Exit();
        }
        private IEnumerator FireBullet()
        {
            while (true)
            {
                targetTrm = Manager.Game.FindToTarget(_enemy.transform, _radius, _whatIsPlayer);
                if (targetTrm == null)
                {
                    _enemy.ChangeState("IDLE");
                }
                else if (_enemy.IsRewind == false)
                {
                    Vector2 playerDirection = (targetTrm.position - _enemy.transform.position).normalized;
                    PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position).GetComponent<Bullet>()
                        .SetMovement(playerDirection, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                }
                yield return new WaitForSeconds(2);
            }
        }
    }
}
