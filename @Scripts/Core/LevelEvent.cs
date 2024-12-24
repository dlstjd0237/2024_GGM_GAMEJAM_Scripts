
namespace BIS.Core
{
    public static class LevelEvent
    {
        public static readonly BossDead BossDeadEvent = new BossDead();
        public static readonly EnemyDead EnemyDeadEvent = new EnemyDead();
    }


    public class BossDead : GameEvent
    {
        public readonly int Exp = 5;
    }
    public class EnemyDead : GameEvent
    {
        public readonly int Exp = 30;
    }
}
