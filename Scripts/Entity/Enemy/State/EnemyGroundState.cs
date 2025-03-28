using BIS.Animators;
using BIS.Core;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyGroundState : EntityState
    {
        protected Enemy _enemy;
        protected EntityMover _mover;
        protected LayerMask _whatIsPlayer;
        protected float _radius;
        public EnemyGroundState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as Enemy;
            _mover = entity.GetCompo<EntityMover>();
            _whatIsPlayer = Define.MLayerMask.WhatIsPlayer;
            _radius = _enemy.PlayerFindRadius;
        }
    }
}
