using BIS.Entities;
using BIS.Init;
using System;
using UnityEngine;
using BIS.Core;
namespace BIS.Managers
{
    public delegate void LevelUpEvent(int currentLevel);
    public delegate void ExpChangeEvent(int currentExp);
    public class EntityLevelSystem : MonoBehaviour, IEntityComponentInit
    {
        public event LevelUpEvent OnLevelUpEvent;
        public event ExpChangeEvent OnExpChangeEvent;

        private Entity _entity;
        [SerializeField] private GameEventChannelSO _levelChannelSO;


        [SerializeField] private int _currentLevel = 1;
        [SerializeField] private int _maxLevel = 100;

        [SerializeField] private int _currentExp;
        [SerializeField] private int _maxExp = 500;

        public void AddExperience(int amount)
        {
            _currentExp += amount;
            while (_currentExp >= _maxExp)
            {
                LevelUp();
            }
            OnExpChangeEvent?.Invoke(_currentExp);
        }

        private void LevelUp()
        {
            _currentExp -= _maxExp;
            _currentLevel++;
            OnLevelUpEvent?.Invoke(_currentLevel);
        }

        public void Initalize(Entity entity)
        {
            _entity = entity;
            _levelChannelSO.AddListener<EnemyDead>(HandleEnemyDead);
            _levelChannelSO.AddListener<BossDead>(HandleBossDead);
        }
        private void OnDestroy()
        {
            _levelChannelSO.RemoveListener<EnemyDead>(HandleEnemyDead);
            _levelChannelSO.RemoveListener<BossDead>(HandleBossDead);
        }

        private void HandleBossDead(BossDead evt)
        {
            AddExperience(evt.Exp);
        }

        private void HandleEnemyDead(EnemyDead evt)
        {
            AddExperience(evt.Exp);
        }
    }
}
