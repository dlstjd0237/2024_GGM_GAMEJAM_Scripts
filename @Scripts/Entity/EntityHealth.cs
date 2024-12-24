using BIS.Init;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BIS.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponentInit
    {
        private Entity _entity;

        public float currentHealth;
        private float maxHealth;

        public event Action OnHit;
        public event Action OnDead;
        public event Action<float> HelathChangeEvent;

        private bool _isMiss = false;
        public void Initalize(Entity entity)
        {
            _entity = entity;
            maxHealth = _entity.Stat.maxHealth.GetValue();
            currentHealth = maxHealth;
            HelathChangeEvent?.Invoke(currentHealth);

        }

        public void Invincibility()
        {
            HelathChangeEvent?.Invoke(currentHealth);
            maxHealth = int.MaxValue;
            currentHealth = maxHealth;
        }

        public void SetIsMissed(bool value)
        {
            _isMiss = value;
        }

        public void ApplyDamage(float damageAmount)
        {
            if (_isMiss)
                return;
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHit?.Invoke();
            HelathChangeEvent?.Invoke(currentHealth);
            if (Mathf.Approximately(currentHealth, 0))
            {
                Debug.Log($"{gameObject.name} Dead");
                OnDead?.Invoke();
            }
        }

    }
}
