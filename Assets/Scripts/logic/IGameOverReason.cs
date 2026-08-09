using space;

namespace logic
{
    public interface IGameOverReason{}

    public class RockHitsPlayerReason : IGameOverReason {}
    public class PlayerWalksIntoMonsterReason : IGameOverReason {}
    public class MonsterCatchesPlayerReason : IGameOverReason {}
    public class ExplosionReason : IGameOverReason {}
}