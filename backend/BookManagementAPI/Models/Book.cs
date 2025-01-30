using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookManagementAPI.Models
{
    public class Book
    {
        //Defining the Book Properties
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("author")]
        public string? Author { get; set; }

        [BsonElement("isbn")]
        public string? ISBN { get; set; }

        [BsonElement("publicationDate")]
        public string? PublicationDate { get; set; }
    }
}
