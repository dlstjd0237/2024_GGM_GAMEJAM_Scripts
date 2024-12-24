using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_player.InputSO.InputDirection.x != 0 || _player.InputSO.InputDirection.y != 0)
                _player.ChangeState("MOVE");
        }
    }
}
