using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
using DG.Tweening;
using BIS.Pool;
using BIS.Objects;

namespace BIS.Enemys
{
    public class EnemyCrashphereIdleState : EntityState
    {
        private EnemyCrashphere _enemy;
        private Vector2 _handDefualtPos;
        private int randomIdx = 0;
        public EnemyCrashphereIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as EnemyCrashphere;
        }

        public override void Enter()
        {
            base.Enter();

            _handDefualtPos = _enemy.handTrm.localPosition;

            randomIdx = randomIdx == 0 ? 1 : 0;
            Sequence seq = DOTween.Sequence();
            seq.Append(_enemy.transform.DOLocalMove(_enemy.movePints[randomIdx], 1).SetEase(Ease.InOutQuad));
            seq.Append(_enemy.handTrm.DOLocalMove(_enemy.handPoints[randomIdx], 0.25f));
            seq.Append(_enemy.handTrm.DOShakePosition(2, new Vector3(0.1f, 0.1f, 0), 20, 150, false, true));
            seq.Join(_enemy.handTrm.DOScale(1.5f, 2));
            seq.Append(_enemy.handTrm.DOLocalMoveY(_enemy.attackPoint.localPosition.y, 0.25f).SetEase(Ease.OutElastic));
            seq.AppendCallback(() =>
            {

                for (int i = 0; i < 24; ++i)
                {
                    GameObject obj = PoolManager.SpawnFromPool("WavePice", _enemy.attackPoint.position);
                    Bullet bullet = obj.GetComponent<Bullet>(); //리지드바디 가져오고

                    Vector3 dir = Vector3.zero;

                    dir = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 24), Mathf.Sin(Mathf.PI * 2 * i / 24), 0);

                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // 방향 벡터의 각도 계산
                    obj.transform.rotation = Quaternion.Euler(0, 0, angle); // Z축 회전 적용

                    bullet.SetMovement(dir.normalized, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());

                }

                for (int i = 0; i < 8; i++)
                {
                    Bullet obj = PoolManager.SpawnFromPool("EnemyCrashphereBullet", _enemy.attackPoint.position).GetComponent<Bullet>();
                    Vector2 dir = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / 8), Mathf.Sin(Mathf.PI * 2 * i / 8), 0);
                    obj.SetMovement(dir, _enemy.Stat.bulletSpeed.GetValue() / 2, _enemy.Stat.attackDamage.GetValue());
                }

            });
            seq.AppendInterval(1);
            seq.Append(_enemy.handTrm.DOScale(1, 2));
            seq.Join(_enemy.handTrm.DOLocalMoveY(_handDefualtPos.y, 0.25f).OnComplete(() => _enemy.ChangeState("IDLE")));
        }

    }
}
