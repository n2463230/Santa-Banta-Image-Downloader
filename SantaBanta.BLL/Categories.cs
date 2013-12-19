namespace SantaBanta.Entity
{
    public class Categories
    {
        public string CategoryName { get; set; }
        public string CategoryURL { get; set; }
    }

    public class CategoriesContent
    {
        public string Name { get; set; }
        public string URL { get; set; }
    }

    public class DownloadList
    {
        public string DownloadImageName { get; set; }
        public string DownloadImageURL { get; set; }
    }

    public enum DatabaseStatus
    {
        Success = 1,
        Error = 2
    }
}
