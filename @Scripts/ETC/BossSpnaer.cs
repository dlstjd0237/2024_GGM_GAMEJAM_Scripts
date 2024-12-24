using BIS.Managers;
using BIS.Pool;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace BIS.Boss
{
    public class BossSponer : MonoBehaviour
    {
        [SerializeField]
        private Transform _spownTrm;
        private void Awake()
        {
            StartCoroutine(SpawnBossCoroutine());
        }

        private IEnumerator SpawnBossCoroutine()
        {
            yield return new WaitForSeconds(3);
            BossType boss = Manager.Game.GetBattleBossType();

            string bossName = boss == BossType.Eye ? "EyeBoss" : boss == BossType.Star ? "StarBoss" : "SkeletonBoss";
            PoolManager.SpawnFromPool("Particle_Spiral", _spownTrm.position);
            DOVirtual.DelayedCall(2, () => PoolManager.SpawnFromPool(bossName, _spownTrm.position));
        }
    }
}
