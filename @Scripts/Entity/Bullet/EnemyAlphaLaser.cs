using UnityEngine;
using DG.Tweening;
using BIS.Pool;

namespace BIS.Objects
{
    public class EnemyAlphaLaser : MonoBehaviour
    {
        public void PlayAlphaLaser(int damage)
        {
            transform.localScale = new Vector3(3, 1, 0);
            transform.DOScaleY(100, 0.5f).OnComplete(() =>
            {
                PoolManager.SpawnFromPool("EnemyLaser", transform.position, transform.rotation).GetComponent<EnemyLaser>().PlayLaser(damage);
                gameObject.SetActive(false);
            });
        }
    }
}
