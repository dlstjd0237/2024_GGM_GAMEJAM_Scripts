using UnityEngine;
using DG.Tweening;
using BIS.Pool;

namespace BIS.Objects
{
    public class Bossorb : MonoBehaviour
    {


        public void PlayOrb(int damage)
        {
            transform.localScale = new Vector3(0, 0, 0);
            transform.DOShakePosition(2, new Vector3(0.1f, 0.1f, 0), 20, 150, false, true).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            transform.DOScale(7f, 3).OnComplete(() =>
            {
                PoolManager.SpawnFromPool("EnemyBiterrainLaser", transform.position).GetComponent<EnemyBiterrainLaser>().PlayLaser(damage);

                gameObject.SetActive(false);
            });

        }


    }
}
