using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace BIS.Enemys
{
    public class EnemyTriangleMoveState : EnemyGroundState
    {
        private bool _isAttack = false;
        private Vector3 _playerDir;
        public EnemyTriangleMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _isAttack = false;
            _enemy.StartCoroutine(StartAttack());

        }
        public override void Update()
        {
            base.Update();

            if (_isAttack == false)
            {
                Transform targetPos = Manager.GameScene.Player.transform;


                Vector2 directionToMouse = (Vector2)targetPos.position - (Vector2)_enemy.transform.position;
                float _angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

                _entity.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));
            }
        }
        private IEnumerator StartAttack()
        {
            Transform targetPos = Manager.GameScene.Player.transform;
            _playerDir = (targetPos.position - _enemy.transform.position).normalized;
            // 1. 뒤로 이동 (준비 동작)
            Vector3 originalPosition = _enemy.transform.position;
            Vector3 backwardPosition = originalPosition - _enemy.transform.right * 2f;

            //Vector2 directionToMouse = (Vector2)targetPos.position - (Vector2)_enemy.transform.position;
            //float _angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            //_enemy.transform.DORotate(new Vector3(0, 0, _angle), 0.5f);
            _enemy.transform.DOMove(backwardPosition, 0.5f).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(0.7f);

            // 2. 돌진 동작
            _isAttack = true;
            _mover.DashMovement(_playerDir, 20);

            yield return new WaitForSeconds(0.3f);
            _mover.StopImmediately();
            yield return new WaitForSeconds(0.5f);
            _isAttack = false;

            // 3. 상태 전환
            _enemy.ChangeState("IDLE");
        }
    }
}
