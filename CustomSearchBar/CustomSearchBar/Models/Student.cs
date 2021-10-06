using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomSearchBar.Models
{
    [Serializable]
    public class Student
    {
        [BsonElement("_id")]
        [JsonProperty("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StudentId { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        [BsonElement("coursId")]
        public List<string> ListFromIdCourse { get; set; }

        public List<string> Countries { get; set; }

        [BsonElement("yourCours"), BsonRepresentation(BsonType.ObjectId)]
        public string CourseIdForgin { get; set; }

    }
}
