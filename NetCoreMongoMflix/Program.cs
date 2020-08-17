using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NetCoreMongoMflix
{
    class Program
    {
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
                                                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                                .Build();

        static void Main(string[] args)
        {
            var connString = Configuration["Mongo:ConnectionString"];

            var client = new MongoClient(connString);

            var db = client.GetDatabase("sample_mflix");
            var collection = db.GetCollection<BsonDocument>("movies");

            var result = collection.Find(new BsonDocument())
               .SortByDescending(m => m["runtime"])
               .Limit(10)
               .ToList();

            foreach(var r in result)
            {
                Console.WriteLine(r.GetValue("title"));
            }
        }
    }
}
