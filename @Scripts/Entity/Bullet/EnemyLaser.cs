using BIS.Entities;
using BIS.Managers;
using DG.Tweening;
using UnityEngine;

namespace BIS.Objects
{
    public class EnemyLaser : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        int _damage;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public void PlayLaser(int damage)
        {

            Manager.Camera.ShakeCamera(new Vector3(3, 3, 3), 3, 3, 1.4f);
            Color c = _spriteRenderer.color;
            c.a = 1;
            _spriteRenderer.color = c;

            transform.localScale = new Vector3(3, 1, 0);
            _damage = damage;
            transform.DOScaleY(100, 0.5f);
            DOVirtual.DelayedCall(1, () => _spriteRenderer.DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false)));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out Entity entity))
            {
                entity.GetCompo<EntityHealth>().ApplyDamage(_damage);
            }
        }
    }
}