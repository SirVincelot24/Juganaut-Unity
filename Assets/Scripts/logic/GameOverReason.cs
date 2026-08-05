using space;

namespace logic
{
    public interface IGameOverReason{}

    public class RockHitsPlayer : IGameOverReason {}
    public class PlayerWalksIntoMonster : IGameOverReason {}
    public class MonsterCatchesPlayer : IGameOverReason {}
    public class Explosion : IGameOverReason {}
}