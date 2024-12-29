import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service'; 

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  books: any[] = [];
  newBook = { id: '', title: '', author: '', isbn: '', publicationDate: '' };

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getBooks(); 
  }

  // Method to get all books from the backend
  getBooks(): void {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data; 
    });
  }

  // Method to add a new book to the backend and reset the form
  addBook(): void {
    
    if (this.newBook.publicationDate) {
      this.newBook.publicationDate = new Date(this.newBook.publicationDate).toISOString().split('T')[0];
    }

    if (this.newBook.id) {
      
      this.bookService.updateBook(this.newBook).subscribe(() => {
        this.getBooks(); 
        this.resetForm();
      });
    } else {
      // Otherwise, add a new book
      this.bookService.addBook(this.newBook).subscribe(() => {
        this.getBooks(); 
        this.resetForm(); 
      });
    }
  }

  // Method to delete a book by ID
  deleteBook(id: string): void {
    this.bookService.deleteBook(id).subscribe(() => {
      this.getBooks(); 
    });
  }

  // Method to pre-fill the form with the data of the selected book for editing
  editBook(book: any): void {
    this.newBook = { ...book }; 
  }

  // Reset the form after adding or updating a book
  resetForm(): void {
    this.newBook = { id: '', title: '', author: '', isbn: '', publicationDate: '' };
  }
}
