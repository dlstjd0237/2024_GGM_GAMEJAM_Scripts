using UnityEngine;
using DG.Tweening;
using BIS.Pool;
namespace BIS.Objects
{
    public class RingAttack : MonoBehaviour
    {
        private void OnEnable()
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        public void Sizeup(float duration)
        {
            transform.DOScale(25, duration).OnComplete(() => gameObject.SetActive(false));
            //PoolManager.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
        }
    }
}
