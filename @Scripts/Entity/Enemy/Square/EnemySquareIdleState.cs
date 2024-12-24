using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;
using System.Collections;

namespace BIS.Enemys
{
    public class EnemySquareIdleState : EnemyGroundState
    {
        public EnemySquareIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(1);
            _enemy.ChangeState("MOVE");
        }

    }
}
