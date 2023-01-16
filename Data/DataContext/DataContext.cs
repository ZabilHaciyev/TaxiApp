using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApp.Data.Context;

namespace TaxiApp.Data.DataContext
{
    public class DataContext : IDataContext
    {
        private readonly IMongoDatabase db;
        public DataContext()
        {
            const string connectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass%20Community&ssl=false";
            var client = new MongoClient(connectionString);
            db = client.GetDatabase("DB");
        }
        public IMongoDatabase GetDatabase()
        {
            return db;
        }
    }
}
