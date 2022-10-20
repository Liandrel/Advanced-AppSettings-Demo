namespace BasicDemo.Options
{
    public class EmailSettingsOptions
    {
        public bool EnableEmailSystem { get; set; }
        public int EmailTimeoutSeconds { get; set; }
        public List<string> EmailServers { get; set; }
        public AdminOptions EmailAdmin { get; set; }
    }
}
