using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookManagementAPI.Models;

namespace BookManagementAPI.Services
{
    public class MongoDbService
    {
        private readonly IMongoCollection<Book> _booksCollection;

        public MongoDbService()
        {
            var client = new MongoClient("mongodb+srv://Siyumi:Siyu2000@booknestcluster.a3x3f.mongodb.net/?retryWrites=true&w=majority&appName=BookNestCluster");
            var database = client.GetDatabase("BookManagementDB");
            _booksCollection = database.GetCollection<Book>("Books");
        }

        public async Task<List<Book>> GetBooksAsync() =>
            await _booksCollection.Find(new BsonDocument()).ToListAsync();

        public async Task AddBookAsync(Book book) =>
            await _booksCollection.InsertOneAsync(book);

        public async Task UpdateBookAsync(string id, Book book) =>
            await _booksCollection.ReplaceOneAsync(b => b.Id == id, book);

        public async Task DeleteBookAsync(string id) =>
            await _booksCollection.DeleteOneAsync(b => b.Id == id);
    }
}
