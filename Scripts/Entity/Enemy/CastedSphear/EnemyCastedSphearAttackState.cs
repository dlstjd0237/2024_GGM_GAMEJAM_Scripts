using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using BIS.Objects;
using BIS.Pool;
using BIS.Managers;

namespace BIS.Enemys
{
    public class EnemyCastedSphearAttackState : EnemyCastedSphearGroundState
    {
        public EnemyCastedSphearAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.StartCoroutine(AttackStartCoroutine());
        }


        private IEnumerator AttackStartCoroutine()
        {
            int randomIdx = Random.Range(0, 2);


            _enemy.transform.DOMoveY(_enemy.movePoint[randomIdx].y, 2f);

            yield return new WaitForSeconds(2);
            _enemy.leftEye.DOFade(1, 0.5f);
            yield return new WaitForSeconds(1);

            // 랜덤 번호 확인
            float startAngle;
            float endAngle;

            if (randomIdx == 1)
            {
                startAngle = -45f; // 아래에서 위로 공격 시작
                endAngle = 45f;    // 위쪽 범위 끝
            }
            else
            {
                startAngle = 45f;  // 위에서 아래로 공격 시작
                endAngle = -45f;   // 아래쪽 범위 끝
            }
            float angleStep = (endAngle - startAngle) / (5 - 1);
            // 첫 번째 공격
            for (int i = 0; i < 5; i++)
            {
                float angle = startAngle + (angleStep * i); // 현재 각도 계산

                // 레이저 생성
                GameObject go = PoolManager.SpawnFromPool("EnemyAlphaLaser", _enemy.leftEye.transform.position, Quaternion.Euler(0, 0, angle));
                go.GetComponent<EnemyAlphaLaser>().PlayAlphaLaser(_enemy.Stat.attackDamage.GetValue());
            }

            yield return new WaitForSeconds(4);

            _enemy.leftEye.DOFade(0, 0.5f);
            _enemy.rightEye.DOFade(1, 0.5f);

            // 두 번째 공격
            for (int i = 0; i < 5; i++)
            {
                float angle = startAngle + (angleStep * i); // 동일한 각도로 공격

                // 레이저 생성
                GameObject go = PoolManager.SpawnFromPool("EnemyAlphaLaser", _enemy.rightEye.transform.position, Quaternion.Euler(0, 0, angle));
                go.GetComponent<EnemyAlphaLaser>().PlayAlphaLaser(_enemy.Stat.attackDamage.GetValue());
            }

            yield return new WaitForSeconds(2);
            _enemy.rightEye.DOFade(0, 0.5f);
            yield return new WaitForSeconds(2);

            _enemy.ChangeState("IDLE");
        }



        public override void Exit()
        {
            _enemy.StopCoroutine(AttackStartCoroutine());
            base.Exit();
        }
    }


}
