using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BookManagementAPI.Models; 
using BookManagementAPI.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BookManagementAPI.Controllers   //Defines the API Controller for Books
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMongoCollection<Book> _bookCollection; //Connecting to the Database MongoDB

        public BooksController(IMongoClient mongoClient)
        {
            
            var database = mongoClient.GetDatabase("bookstoreDB"); 
            _bookCollection = database.GetCollection<Book>("books"); 
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                // Fetch books from MongoDB collection
                var books = await _bookCollection.Find(book => true).ToListAsync();
                return Ok(books); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Book object is null.");

            if (string.IsNullOrEmpty(book.Title) || string.IsNullOrEmpty(book.Author))
                return BadRequest("Book must have a title and an author.");

            try
            {
                // Create the book and insert it into MongoDB
                await _bookCollection.InsertOneAsync(book);
                return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/books/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(string id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest("Updated book object is null.");

            if (string.IsNullOrEmpty(updatedBook.Title) || string.IsNullOrEmpty(updatedBook.Author))
                return BadRequest("Updated book must have a title and an author.");

            var book = await _bookCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
                return NotFound($"Book with ID {id} not found.");

            try
            {
                // Update book properties
                var updateDefinition = Builders<Book>.Update
                    .Set(b => b.Title, updatedBook.Title)
                    .Set(b => b.Author, updatedBook.Author)
                    .Set(b => b.ISBN, updatedBook.ISBN)
                    .Set(b => b.PublicationDate, updatedBook.PublicationDate);

                var result = await _bookCollection.UpdateOneAsync(b => b.Id == id, updateDefinition);

                if (result.ModifiedCount > 0)
                {
                    return NoContent(); 
                }
                else
                {
                    return BadRequest("Book update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var book = await _bookCollection.Find(b => b.Id == id).FirstOrDefaultAsync();
            if (book == null)
                return NotFound($"Book with ID {id} not found.");

            try
            {
                // Delete the book from the collection
                await _bookCollection.DeleteOneAsync(b => b.Id == id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
