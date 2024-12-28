import { Component, OnInit } from '@angular/core';
import { BookService } from '../book.service';  // Ensure this is the correct path

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent implements OnInit {
  books: any[] = [];
  newBook = { id: 0, title: '', author: '', isbn: '', publicationDate: '' };

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks(): void {
    this.bookService.getBooks().subscribe((data: any) => {
      this.books = data;
    });
  }

  addBook(): void {
    this.bookService.addBook(this.newBook).subscribe(() => {
      this.getBooks();
      this.newBook = { id: 0, title: '', author: '', isbn: '', publicationDate: '' }; // Reset form
    });
  }

  deleteBook(id: number): void {
    this.bookService.deleteBook(id).subscribe(() => {
      this.getBooks();
    });
  }
}
