using BIS.Entities;
using BIS.Init;
using BIS.Pool;
using BIS.Rewinds;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BIS.Core.Define;

namespace BIS.Objects
{
    public class Bullet : InitBase
    {
        protected Rigidbody2D _rigidbody2D;
        protected Collider2D _collider2D;
        protected SpriteRenderer _render;
        [SerializeField]
        protected EObjectTag _targetTag;
        private int _dmaamge;

        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<Collider2D>();
            _render = GetComponent<SpriteRenderer>();

            return true;
        }

        private void OnEnable()
        {
            _collider2D.enabled = true;
            _render.enabled = true;
        }

        public void SetMovement(Vector2 dir, float speed, int damage)
        {
            StartCoroutine(ObejctActive(false, 3));
            _rigidbody2D.linearVelocity = dir * speed;
            _dmaamge = damage;
        }

        private IEnumerator ObejctActive(bool value, float time)
        {
            yield return new WaitForSeconds(time);
            gameObject.SetActive(value);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(_targetTag.ToString()) && collision.TryGetComponent<Entity>(out Entity entity))
            {
                entity.GetCompo<EntityHealth>().ApplyDamage(_dmaamge);
                _collider2D.enabled = false;
                _render.enabled = false;

                collisionEvent(collision);
            }
        }

        protected virtual void collisionEvent(Collider2D collision)
        {
            Vector3 randomVector;

            PoolManager.SpawnFromPool("Particle_Attack", collision.transform.position);

            gameObject.SetActive(false);
        }
    }
}
