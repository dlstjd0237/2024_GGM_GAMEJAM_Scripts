using BIS.Animators;
using BIS.Entities;
using BIS.FSM;
using UnityEngine;

namespace BIS.Enemys
{
    public class EnemyCastedSphearGroundState : EntityState
    {
        protected EnemyCastedSphear _enemy;

        public EnemyCastedSphearGroundState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _enemy = entity as EnemyCastedSphear;

        }
    }
}
