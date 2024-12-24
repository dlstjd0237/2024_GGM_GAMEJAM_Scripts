using UnityEngine;
using BIS.Init;
using BIS.Entities;
using System;
using System.Collections.Generic;

namespace BIS.Skills
{
    public delegate void CooldownInfoEvent(float current, float total);
    public class Skill : MonoBehaviour, IEntityComponentInit
    {
        [Header("SkillInfo")]
        public bool _skillEnabled = false;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected int _maxCheckEnemy = 3;
        [SerializeField] protected int _maxSkillUpgradeCount = 5;

        public bool IsCooldown => _cooldownTimer > 0f;

        protected int _currentSkillUpgradeCount = 0;
        protected float _cooldownTimer;
        protected Entity _entity;
        protected Collider[] _colliders;

        public CooldownInfoEvent OnCooldownEvent;
        public event Action StartCooldownEvent;
        public event Action EndCooldownEvent;

        public void Initalize(Entity entity)
        {
            _entity = entity;
            _colliders = new Collider[_maxCheckEnemy];
        }

        protected virtual void Update()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;

                if (_cooldownTimer <= 0)
                {
                    EndCooldownEvent?.Invoke();

                    _cooldownTimer = 0;
                }

                OnCooldownEvent?.Invoke(_cooldownTimer, _cooldown);
            }
        }

        public virtual bool TryUpgradeSkill()
        {
            if (_currentSkillUpgradeCount < _maxSkillUpgradeCount)
            {
                _currentSkillUpgradeCount++;
                UpgradeSkill();
                return true;
            }

            return false;
        }

        protected virtual void UpgradeSkill()
        {
        }
    }
}
