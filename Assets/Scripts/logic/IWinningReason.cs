namespace logic
{
    public interface IWinningReason {}

    public class AllDiamondsCollected : IWinningReason
    {
        public readonly int DiamondsCollected;

        public AllDiamondsCollected(int diamondsCollected)
        {
            DiamondsCollected = diamondsCollected;
        }
    }
}