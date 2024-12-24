using BIS.Animators;
using BIS.Entities;
using BIS.Objects;
using BIS.Pool;
using DG.Tweening;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyHexagonMoveState : EnemyGroundState
    {
        private int _spawnCount = 12;
        public EnemyHexagonMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            SpriteRenderer renderer = _entity.GetCompo<EntityRenderer>().Renderer;
            Color defualtColor = renderer.color;
            Sequence seq = DOTween.Sequence();
            seq.Append(renderer.DOColor(Color.white, 0.5f));
            seq.Append(renderer.DOColor(defualtColor, 1.0f));
            seq.Append(renderer.DOColor(Color.white, 0.5f));
            seq.Append(renderer.DOColor(defualtColor, 1.0f));
            seq.AppendCallback(() =>
            {
                //�̰��� ���� ����Ʈ �߰�   
                PoolManager.SpawnFromPool("Particle_Pop", _enemy.transform.position);
                for (int i = 0; i < _spawnCount; ++i)
                {
                    GameObject obj = PoolManager.SpawnFromPool("EnemyBullet", _enemy.transform.position);
                    Bullet rig = obj.GetComponent<Bullet>(); //������ٵ� ��������

                    Vector3 dir = Vector3.zero;

                    dir = new Vector3(Mathf.Cos(Mathf.PI * 2 * i / _spawnCount), Mathf.Sin(Mathf.PI * 2 * i / _spawnCount), 0);
                    rig.SetMovement(dir, _enemy.Stat.bulletSpeed.GetValue(), _enemy.Stat.attackDamage.GetValue());
                }
                _enemy.gameObject.SetActive(false);
            });
        }
    }
}
