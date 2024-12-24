using System.Collections.Generic;
using UnityEngine;
using System;
using BIS.Init;
using BIS.Pool;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using DG.Tweening;

namespace BIS.Enemys
{
    [Serializable]
    public struct EnemySpawnData
    {
        public Transform SpawnTrm1;
        public Transform SpawnTrm2;
        public EnemyTagNameSO EnemyTagSO;
    }

    [Serializable]
    public struct SpawnCombination
    {
        public Action CombinationMethod;
        public float Probability; // 각 조합의 등장 확률
    }

    public enum EnemyType
    {
        Skeleton,
        CastedSphear,
        Crashphere,
        Hexagon,
        Square,
        Triangle,
        Biterrain
    }


    public class EnemySpawner : InitBase
    {
        [SerializeField] private List<EnemySpawnData> _spawnDataList;
        private Dictionary<EnemyType, EnemySpawnData> _enemySpawnTagDictionary;
        private List<SpawnCombination> _combinations = new List<SpawnCombination>();
        public override bool Init()
        {
            if (base.Init() == false)
                return false;

            _enemySpawnTagDictionary = new();

            for (int i = 0; i < _spawnDataList.Count; ++i)
            {
                _enemySpawnTagDictionary.Add(_spawnDataList[i].EnemyTagSO.EnemyType, _spawnDataList[i]);
            }

            _combinations.Add(new SpawnCombination { CombinationMethod = Combination1, Probability = 0.1379f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination2, Probability = 0.1034f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination3, Probability = 0.0689f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination4, Probability = 0.1379f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination5, Probability = 0.0689f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination6, Probability = 0.0345f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination7, Probability = 0.1379f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination8, Probability = 0.1379f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination9, Probability = 0.0689f });
            _combinations.Add(new SpawnCombination { CombinationMethod = Combination10, Probability = 0.1034f });

            float totalProbability = 0f;
            foreach (var combination in _combinations)
            {
                totalProbability += combination.Probability;
            }
            if (Math.Abs(totalProbability - 1.0f) > 0.01f)
            {
                Debug.LogError("확률의 합이 100%가 아닙니다. 각 조합의 확률을 확인하세요.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 랜덤으로 조합 실행
        /// </summary>
        public void SpawnRandomCombination()
        {
            float randomValue = Random.value; // 0.0 ~ 1.0 사이의 값
            float cumulativeProbability = 0f;

            foreach (var combination in _combinations)
            {
                cumulativeProbability += combination.Probability;
                if (randomValue <= cumulativeProbability)
                {
                    combination.CombinationMethod.Invoke();
                    return;
                }
            }
        }
        private void Spawn(int count, EnemyType enemyType)
        {
            for (int i = 0; i < count; ++i)
            {
                int idx = Random.Range(0, 2);
                Transform trm = idx == 0 ? _enemySpawnTagDictionary[enemyType].SpawnTrm1 : _enemySpawnTagDictionary[enemyType].SpawnTrm2;
                PoolManager.SpawnFromPool("Particle_Spiral", trm.position);
                DOVirtual.DelayedCall(2, () => PoolManager.SpawnFromPool(_enemySpawnTagDictionary[enemyType].EnemyTagSO.Tag, trm.position));
            }
        }
        /// <summary>
        /// 40%
        /// </summary>
        public void Combination1()
        {
            Spawn(4, EnemyType.Triangle);
            Spawn(1, EnemyType.CastedSphear);
            Spawn(3, EnemyType.Hexagon);
        }

        /// <summary>
        /// 30%
        /// </summary>
        public void Combination2()
        {
            Spawn(2, EnemyType.CastedSphear);
            Spawn(2, EnemyType.Crashphere);
        }


        /// <summary>
        /// 20%
        /// </summary>
        public void Combination3()
        {
            Spawn(8, EnemyType.Hexagon);
        }

        /// <summary>
        /// 40%
        /// </summary>
        public void Combination4()
        {
            Spawn(1, EnemyType.Biterrain);
            Spawn(2, EnemyType.Crashphere);
            Spawn(2, EnemyType.Square);
        }

        /// <summary>
        /// 20%
        /// </summary>
        public void Combination5()
        {
            Spawn(1, EnemyType.Biterrain);
            Spawn(1, EnemyType.Triangle);
            Spawn(1, EnemyType.Crashphere);
            Spawn(1, EnemyType.Square);
            Spawn(1, EnemyType.CastedSphear);
        }

        /// <summary>
        /// 10%  
        /// </summary>
        public void Combination6()
        {
            Spawn(1, EnemyType.Biterrain);
            Spawn(2, EnemyType.Triangle);
            Spawn(2, EnemyType.Crashphere);
            Spawn(2, EnemyType.CastedSphear);
        }

        /// <summary>
        /// 40%  
        /// </summary>
        public void Combination7()
        {
            Spawn(4, EnemyType.Square);
            Spawn(4, EnemyType.Triangle);
        }

        /// <summary>
        /// 40%  
        /// </summary>
        public void Combination8()
        {
            Spawn(1, EnemyType.Biterrain);
            Spawn(1, EnemyType.Crashphere);
        }

        /// <summary>
        /// 20%  
        /// </summary>
        public void Combination9()
        {
            Spawn(2, EnemyType.CastedSphear);
            Spawn(2, EnemyType.Crashphere);
            Spawn(2, EnemyType.Triangle);
            Spawn(1, EnemyType.Square);
        }


        /// <summary>
        /// 30%  
        /// </summary>
        public void Combination10()
        {
            Spawn(2, EnemyType.Triangle);
            Spawn(4, EnemyType.Square);
            Spawn(1, EnemyType.Biterrain);
        }


    }
}
