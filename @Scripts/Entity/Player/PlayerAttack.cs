using BIS.Enemys;
using BIS.Entities;
using BIS.Managers;
using UnityEngine;

namespace BIS.Players
{
    public class PlayerAttack : MonoBehaviour
    {
        private Animator _animator;
        private Collider2D _collider2D;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();
            _collider2D.enabled = false;
        }

        public void Attack()
        {
            _animator.SetTrigger("isAttack");
            _collider2D.enabled = true;
        }

        public void AnimationEnd()
        {
            _collider2D.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && collision.TryGetComponent(out Enemy enemy))
            {
                Manager.Camera.ShakeCamera(Vector2.one * 2, 3, 3, 0.7f);
                enemy.GetCompo<EntityHealth>().ApplyDamage(1);
            }

            if (collision.CompareTag("EnemyHommingBullet") && collision.TryGetComponent(out HomingBullet ho))
            {
                Manager.Camera.ShakeCamera(Vector2.one * 2, 3, 3, 0.7f);
                ho.gameObject.SetActive(false);
            }
        }

    }
}
