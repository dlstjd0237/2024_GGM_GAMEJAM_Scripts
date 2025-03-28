using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyTriangleIdleState : EnemyGroundState

    {
        public EnemyTriangleIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            Transform targetPos = Manager.GameScene.Player.transform;

            Vector2 playerDirection = (targetPos.position - _enemy.transform.position).normalized;
            _mover.SetMovement(playerDirection);

            Vector2 directionToMouse = (Vector2)targetPos.position - (Vector2)_enemy.transform.position;
            float _angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            _entity.transform.rotation = Quaternion.Euler(new Vector3(0, 0, _angle));

            Transform playerTrm = Manager.Game.FindToTarget(_enemy.transform, _radius, _whatIsPlayer);
            if (playerTrm != null)
            {
                _enemy.ChangeState("CHASE");
            }
        }
    }
}
