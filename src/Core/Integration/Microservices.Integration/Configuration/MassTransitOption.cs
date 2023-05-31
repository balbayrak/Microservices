namespace Microservices.Integration.Configuration
{
    public class MassTransitOption
    {
        public List<MassTransitBusOption> MassTransitBusOptions { get; set; }
        public string BusBrokerName { get; set; }
        public int BusStartStartTimeoutSeconds { get; set; }
        public int BusStartStopTimeoutSeconds { get; set; }
        public int RetryLimitCount { get; set; }
        public int InitialIntervalSeconds { get; set; }
        public int IntervalIncrementSeconds { get; set; }
        public int ConcurrencyLimit { get; set; }
        public bool UseAutomaticCorrelation { get; set; } = true;

        public MassTransitBusOption SelectedMassTransitBusOption()
        {
            if (MassTransitBusOptions == null)
                throw new ArgumentNullException(nameof(MassTransitBusOptions));

            if (!MassTransitBusOptions.Any())
                throw new ArgumentException($"{nameof(MassTransitBusOptions)} is empty");

            MassTransitBusOption massTransitBusOption = MassTransitBusOptions.FirstOrDefault(p => p.BrokerName == BusBrokerName);

            if (massTransitBusOption == null)
                throw new ArgumentOutOfRangeException($"MassTransitOption could not found. {nameof(BusBrokerName)} : {BusBrokerName}");

            return massTransitBusOption;
        }
    }
}
