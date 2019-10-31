namespace Shared.ClientSideStorage
{
    class StorageConfig
    {
        public string Name { get; set; }
        public int Version { get; set; } 
        public int Size { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string[] DriverOrder { get; set; }
        public string DbKey { get; set; }
    }
}
