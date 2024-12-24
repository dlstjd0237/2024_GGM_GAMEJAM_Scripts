using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using BIS.Managers;
using System;
using UnityEngine;
using static BIS.Core.Define;

namespace BIS.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        private Vector2 inputDirection = Vector2.zero;
        public PlayerMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }


        public override void Update()
        {
            base.Update();
            if (Mathf.Approximately(inputDirection.x, 0) && Mathf.Approximately(inputDirection.y, 0))
            {
                _player.ChangeState("IDLE");
            }
        }

        public override void FixedUpdate()
        {
            inputDirection = _player.InputSO.InputDirection;
            _mover.SetMovement(inputDirection);
            base.FixedUpdate();
        }

    }
}
