using UnityEngine;

namespace BIS.Enemys
{
    [CreateAssetMenu(fileName = "EnemyTagNameSO", menuName = "SO/BIS/EnemyTagNameSO")]
    public class EnemyTagNameSO : ScriptableObject
    {
        [SerializeField] private string _tag; public string Tag { get { return _tag; } }
        [SerializeField] private EnemyType _enemyType; public EnemyType EnemyType { get { return _enemyType; } }
        private void OnValidate()
        {
            _tag = this.name;
        }
    }
}
