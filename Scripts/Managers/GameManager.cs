using UnityEngine;

namespace BIS.Managers
{
    public enum BossType
    {
        Skeleton,
        Star,
        Eye,
        None
    }
    public class GameManager
    {

        public bool IsBoss1Visit { get; set; } = false;
        public bool IsBoss2Visit { get; set; } = false;
        public bool IsBoss3Visit { get; set; } = false;

        public Transform FindToTarget(Transform checkTransform, float radius, LayerMask targetLayer)
        {
            Collider2D collider = Physics2D.OverlapCircle(checkTransform.position, radius, targetLayer);

            return collider == null ? null : collider.transform;
        }

        public BossType GetBattleBossType()
        {
            if (IsBoss1Visit == false)
            {
                IsBoss1Visit = true;
                return BossType.Eye;
            }

            if (IsBoss2Visit == false)
            {
                IsBoss2Visit = true;
                return BossType.Star;
            }

            if (IsBoss3Visit == false)
            {
                IsBoss3Visit = true;
                return BossType.Skeleton;
            }

            return BossType.None;
        }

    }
}
