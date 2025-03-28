using BIS.Core;
using BIS.Entities;
using BIS.FSM;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BIS.Enemys
{
    public class Enemy : Entity
    {
        public List<StateSO> states;
        [field: SerializeField] public float PlayerFindRadius { get; private set; }

        protected StateMachine _stateMachine;

        [SerializeField] private bool _isRotate = true;
        [SerializeField] private GameEventChannelSO _enemyDeadSO;

        [SerializeField] private SpriteRenderer[] _spriteRenderers;

        protected override void AfterInitalize()
        {
            base.AfterInitalize();
            _stateMachine = new StateMachine(states, this);

            EntityHealth health = GetCompo<EntityHealth>();


            health.OnDead += HandleDeadEvent;
            health.OnHit += HandleHitEvent;
        }

        private void HandleHitEvent()
        {
            for (int i = 0; i < _spriteRenderers.Length; ++i)
            {
                Color defualtColor = _spriteRenderers[i].color;
                DOVirtual.DelayedCall(0.15f, () =>
                 {
                     _spriteRenderers[i].color = Color.white;
                 }).OnComplete(() => _spriteRenderers[i].color = defualtColor);
            }
        }

        private void Start()
        {
            _stateMachine.Initalize("IDLE"); //IDLE상태로 시작
            EaseRotate();
        }

        private void EaseRotate()
        {
            if (_isRotate == true)
                transform.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.FastBeyond360).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo);
        }

        private void HandleDeadEvent()
        {
            _enemyDeadSO.RaiseEvent(LevelEvent.EnemyDeadEvent);
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        public void ChangeState(string stateName) => _stateMachine.ChangeState(stateName);

        protected override void Update()
        {
            base.Update();
            _stateMachine.UpdateFSM();
        }

        protected override void OnDestroy()
        {
            EntityHealth health = GetCompo<EntityHealth>();
            health.OnDead += HandleDeadEvent;
            health.OnHit += HandleHitEvent;
            base.OnDestroy();
        }
        protected override void OnDisable()
        {
            StopAllCoroutines();
            base.OnDisable();
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, PlayerFindRadius);
            Gizmos.color = Color.white;
        }

#endif



    }
}
