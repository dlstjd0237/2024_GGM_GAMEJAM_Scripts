using BIS.Animators;
using BIS.Core;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyIdleState : EnemyGroundState
    {
        public EnemyIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {

        }

        public override void Update()
        {
            base.Update();
            Transform targetPos = Manager.GameScene.Player.transform;
            Vector2 playerDirection = (targetPos.position - _enemy.transform.position).normalized;
            _mover.SetMovement(playerDirection);

            Transform playerTrm = Manager.Game.FindToTarget(_enemy.transform, _radius, _whatIsPlayer);
            if (playerTrm != null)
            {
                _enemy.ChangeState("CHASE");
            }
        }


    }
}
