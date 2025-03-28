using BIS.Entities;
using BIS.FSM;
using BIS.Inputs;
using BIS.Managers;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIS.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO InputSO;
        [field: SerializeField] public PlayerAttack PlayerAttack;
        public List<StateSO> states;

        private StateMachine _stateMachine;
        private EntityMover _mover;
        private Collider2D _collider;

        [Space(2)]
        [Header("SkillInfo")]
        [SerializeField] private float _dashCoolTime = 0.5f;
        private float _dashLastUseTime = 0;

        [Space(1)]
        [SerializeField] private float _rewindCoolTime = 60;
        private float _rewindLastUseTime = 0;

        [Space(1)]
        [SerializeField] private float _slowCoolTime = 60;
        private float _slowLastUseTime = 0;


        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            IsRewindObject = false;

            _collider = GetComponent<Collider2D>();

            Manager.GameScene.SetPlayer(this);

            InputSO.ESkillEvent += HandleUseESkill;
            InputSO.QSkillEvent += HandleUseQSkill;
            InputSO.JumpEvent += HandleDashEvent;
            return true;
        }
        protected override void AfterInitalize()
        {
            base.AfterInitalize();
            _stateMachine = new StateMachine(states, this);
            _mover = GetCompo<EntityMover>();
            GetCompo<EntityHealth>().OnDead += HandleDeadEvent;

        }

        private void HandleDeadEvent()
        {
            SceneControlManager.FadeOut(delegate { SceneManager.LoadScene("DeadScene"); });
        }

        private void Start()
        {
            _stateMachine.Initalize("IDLE"); //IDLE상태로 시작
        }

        public void ChangeState(string stateName) => _stateMachine.ChangeState(stateName);

        protected override void Update()
        {
            base.Update();
            _stateMachine.UpdateFSM();

            if (Input.GetKey(KeyCode.I))
            {
                GetCompo<EntityHealth>().Invincibility();
            }

        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _stateMachine.FixedUpdateFSM();
        }

        public void HandleDashEvent()
        {
            if (_dashLastUseTime + _dashCoolTime <= Time.time)
            {
                GetCompo<EntityHealth>().SetIsMissed(true);
                DOVirtual.DelayedCall(_dashCoolTime, delegate { GetCompo<EntityHealth>().SetIsMissed(false); });
                PlayerAttack.Attack();
                _mover.DashMovement(InputSO.InputDirection, Stat.dashSpeed.GetValue());
                Debug.Log("대쉬함");
                _dashLastUseTime = Time.time;
            }
        }
        private void HandleUseESkill()
        {
            if (_rewindLastUseTime + _rewindCoolTime <= Time.time)
            {
                _mover.StopImmediately();
                Manager.Rewind.AllRewind();

                _rewindLastUseTime = Time.time;
            }
        }

        private void HandleUseQSkill()
        {
            if (_slowLastUseTime + _slowCoolTime <= Time.time)
            {
                Manager.Rewind.AllSlow();

                _slowLastUseTime = Time.time;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                GetCompo<EntityHealth>().ApplyDamage(1);
                Manager.Camera.ShakeCamera(Vector3.one, 3, 3, 0.2f);
            }
        }
        protected override void OnDestroy()
        {
            GetCompo<EntityHealth>().OnDead -= HandleDeadEvent;
            InputSO.ESkillEvent -= HandleUseESkill;
            InputSO.QSkillEvent -= HandleUseQSkill;
            InputSO.JumpEvent -= HandleDashEvent;
        }

    }
}
