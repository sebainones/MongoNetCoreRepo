using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NetCoreMongoMflix
{
    class Program
    {
        static void Main(string[] args)
        {

            var connString = "mongodb+srv://m220student:m220password@mflix.abmld.azure.mongodb.net/test?retryWrites=true&w=majority";

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
