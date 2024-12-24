using BIS.Animators;
using BIS.Entities;
using BIS.Managers;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyHexagonIdleState : EnemyGroundState
    {
        private float _randomRange;
        public EnemyHexagonIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _randomRange = Random.Range(5, 16);

        }
        public override void Update()
        {
            base.Update();
            Transform targetPos = Manager.GameScene.Player.transform;
            Vector2 playerDirection = (targetPos.position - _enemy.transform.position).normalized;
            _mover.SetMovement(playerDirection);

            Transform playerTrm = Manager.Game.FindToTarget(_enemy.transform, _randomRange, _whatIsPlayer);
            if (playerTrm != null)
            {
                _mover.StopImmediately();
                _enemy.ChangeState("MOVE");
            }
        }
    }
}
