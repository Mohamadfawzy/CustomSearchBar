using CustomSearchBar.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace CustomSearchBar.Services
{
    public class MongoDBService
    {
        string connectionString = @"mongodb://mfxamarin:S1ebrG1a5nepWCsSndqE72aARLKwCu8xAcUshjRbVpCuH6fL8WjqfA2oupKfobSbIkb2Y8KwzirQfJG9hyvoWg==@mfxamarin.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@mfxamarin@";
        //string connectionString = @"mongodb+srv://mfh101mongo:mfh101password@mfhla.v9qpu.mongodb.net/test";

        string dbName = "MFXamarin1";
        //string dbName = "sample_mflix";
        string collectionName = "users";

        IMongoCollection<Student> tasksCollection;
        IMongoCollection<Student> TasksCollection
        {
            get
            {
                if (tasksCollection == null)
                {
                    var mongoUrl = new MongoUrl(connectionString);

                    // APIKeys.Connection string is found in the portal under the "Connection String" blade
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      mongoUrl
                    );

                    settings.SslSettings =
                        new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                    settings.RetryWrites = false;

                    // Initialize the client
                    var mongoClient = new MongoClient(settings);

                    // This will create or get the database
                    var db = mongoClient.GetDatabase(dbName);

                    // This will create or get the collection
                    var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
                    tasksCollection = db.GetCollection<Student>(collectionName, collectionSettings);
                }
                return tasksCollection;
            }
        }
        public async Task CreateTask(string Name)
        {
            //var usersDocument = client.GetDatabase("sample_mflix").GetCollection<Student>("users");
            //usersDocument.InsertOne(student);
            var student = new Student() { name = Name, age = 40 };
            await TasksCollection.InsertOneAsync(student);
            var result = TasksCollection.Find<Student>(x=>x.StudentId == student.StudentId).FirstOrDefault();
        }


    }
}
