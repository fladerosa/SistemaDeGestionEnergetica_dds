using MongoDB.Driver;
using System.Configuration;

namespace SGE.WebconAutenticacion.App_Start
{
    public class MongoDBContext
    {
        MongoClient client;
        public IMongoDatabase database;

        public MongoDBContext()
        {
            client = new MongoClient(ConfigurationManager.AppSettings["MongoDBHost"]);
            database = client.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);

        }
    }
}