using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using DG.Tweening;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyCastedSphearIdleState : EnemyCastedSphearGroundState
    {
        public EnemyCastedSphearIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            DOVirtual.DelayedCall(2, () => _enemy.ChangeState("ATTACK"));
         
        }

    }
}
