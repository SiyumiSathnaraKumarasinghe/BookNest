// File: Services/BookService.cs
using MongoDB.Driver;
using BookManagementAPI.Models;

namespace BookManagementAPI.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _bookCollection;

        public BookService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("bookstoreDB"); // Replace with your MongoDB database name
            _bookCollection = database.GetCollection<Book>("books"); // Replace with your MongoDB collection name
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _bookCollection.Find(book => true).ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookCollection.InsertOneAsync(book);
        }

        public async Task DeleteBookAsync(string id)
        {
            await _bookCollection.DeleteOneAsync(book => book.Id == id);
        }
    }
}
