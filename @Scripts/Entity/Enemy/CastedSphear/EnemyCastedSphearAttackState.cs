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

            // ���� ��ȣ Ȯ��
            float startAngle;
            float endAngle;

            if (randomIdx == 1)
            {
                startAngle = -45f; // �Ʒ����� ���� ���� ����
                endAngle = 45f;    // ���� ���� ��
            }
            else
            {
                startAngle = 45f;  // ������ �Ʒ��� ���� ����
                endAngle = -45f;   // �Ʒ��� ���� ��
            }
            float angleStep = (endAngle - startAngle) / (5 - 1);
            // ù ��° ����
            for (int i = 0; i < 5; i++)
            {
                float angle = startAngle + (angleStep * i); // ���� ���� ���

                // ������ ����
                GameObject go = PoolManager.SpawnFromPool("EnemyAlphaLaser", _enemy.leftEye.transform.position, Quaternion.Euler(0, 0, angle));
                go.GetComponent<EnemyAlphaLaser>().PlayAlphaLaser(_enemy.Stat.attackDamage.GetValue());
            }

            yield return new WaitForSeconds(4);

            _enemy.leftEye.DOFade(0, 0.5f);
            _enemy.rightEye.DOFade(1, 0.5f);

            // �� ��° ����
            for (int i = 0; i < 5; i++)
            {
                float angle = startAngle + (angleStep * i); // ������ ������ ����

                // ������ ����
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
