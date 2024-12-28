import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service'; // Ensure this is the correct path

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  books: any[] = [];
  newBook = { id: '', title: '', author: '', isbn: '', publicationDate: '' }; // Default structure for newBook

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getBooks(); // Get the list of books when the component is initialized
  }

  // Method to get all books from the backend
  getBooks(): void {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data; // Assign fetched data to the books array
    });
  }

  // Method to add a new book to the backend and reset the form
  addBook(): void {
    // Convert the publicationDate to ISO 8601 format (yyyy-MM-dd)
    if (this.newBook.publicationDate) {
      this.newBook.publicationDate = new Date(this.newBook.publicationDate).toISOString().split('T')[0];
    }

    if (this.newBook.id) {
      // If there's an ID, update the existing book
      this.bookService.updateBook(this.newBook).subscribe(() => {
        this.getBooks(); // Refresh the list of books after updating
        this.resetForm(); // Reset the form
      });
    } else {
      // Otherwise, add a new book
      this.bookService.addBook(this.newBook).subscribe(() => {
        this.getBooks(); // Refresh the list of books after adding
        this.resetForm(); // Reset the form
      });
    }
  }

  // Method to delete a book by ID
  deleteBook(id: string): void {
    this.bookService.deleteBook(id).subscribe(() => {
      this.getBooks(); // Refresh the list of books after deletion
    });
  }

  // Method to pre-fill the form with the data of the selected book for editing
  editBook(book: any): void {
    this.newBook = { ...book }; // Copy the book's data into the form
  }

  // Reset the form after adding or updating a book
  resetForm(): void {
    this.newBook = { id: '', title: '', author: '', isbn: '', publicationDate: '' }; // Reset to initial state
  }
}
