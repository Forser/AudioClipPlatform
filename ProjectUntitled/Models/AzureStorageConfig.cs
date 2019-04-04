namespace ProjectUntitled.Models
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string QueueName { get; set; }
        public string ClipsContainer { get; set; }

        public string StorageConnectionString { get; set; }
    }
}
