using BIS.Animators;
using BIS.Entities;
using BIS.Managers;
using BIS.Objects;
using BIS.Pool;
using DG.Tweening;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemySquareMoveState : EnemyGroundState
    {
        public EnemySquareMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Transform targetPos = Manager.GameScene.Player.transform;

            Vector3 playerDirection = (targetPos.position - _enemy.transform.position).normalized;
            _enemy.transform.DOScale(2.5f, 0.5f).OnComplete(() =>
             {
                 _enemy.transform.DOMove(_enemy.transform.position + playerDirection * 5, 0.5f).OnComplete(() =>
                 {
                     _enemy.transform.DOScale(3f, 0.5f).SetEase(Ease.InOutBack);
                     PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position).GetComponent<Bullet>().
                     SetMovement(Vector2.up, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                     PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position).GetComponent<Bullet>().
                   SetMovement(Vector2.down, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                     PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position).GetComponent<Bullet>().
                   SetMovement(Vector2.left, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                     PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position).GetComponent<Bullet>().
                   SetMovement(Vector2.right, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                     _enemy.ChangeState("IDLE");
                     //ÃÑ¾Ë »ý¼º
                 });
             });
        }
    }
}
