using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentManagement.Models
{
    /*What to do if the JSON document in MongoDB contains more fields than the properties in the corresponding C# class? 
      Well, we can use [BsonIgnoreExtraElements] attribute and instruct the serializer to ignore the extra elements.*/
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]//attribute specifies that this is the Id field or property.
        [BsonRepresentation(BsonType.ObjectId)]//attribute automatically converts Mongo data type to a .Net data type and vice-versa.
        public string? Id { get; set; }

        [BsonElement("name")]//attribute specifies the field in the Mongo document the decorated property corresponds to.
        public string? Name { get; set; }

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("cources")]
        public string[]? Courses { get; set; }

        [BsonElement("gender")]
        public string? Gender { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }
    }
}
