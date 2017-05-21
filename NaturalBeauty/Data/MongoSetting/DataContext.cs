using MongoDB.Driver;

namespace Data.MongoSetting
{
    public class DataContext
    {

        public DataContext() { }

        public DataContext(MongoUrl url)
        {
            Client = new MongoClient(url);
            DatabaseName = url.DatabaseName;
        }

        public DataContext(string connectionString)
        {
            //take database name from connection string
            Client = new MongoClient(connectionString);
            DatabaseName = MongoUrl.Create(connectionString).DatabaseName;
        }

        public string DatabaseName { get; set; }
        public MongoClient Client { get; set; }
    }
}
