namespace Wsa.Gaas.GobbletGobblers.Domain
{
    public class PutCockCommand
    {
        public Guid PlayerId { get; private set; }

        public int HandCockIndex { get; private set; }

        public Location Location { get; private set; }

        public PutCockCommand(Guid playerId, int handCockIndex, Location location)
        {
            PlayerId = playerId;
            HandCockIndex = handCockIndex;
            Location = location;
        }
    }
}