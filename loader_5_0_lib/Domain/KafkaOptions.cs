namespace loader_5_0_lib.Domain
{
    public class KafkaOptions
    {
        public bool Active;
        public string? BootstrapServers { get; set; }
        public int? MessageMaxBytes { get; set; }
        public Dictionary<string, string> Topics { get; set; } = [];
    }
}
