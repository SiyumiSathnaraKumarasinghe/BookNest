import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';  // Import HttpClientModule
import { FormsModule } from '@angular/forms';  // Import FormsModule
import { AppComponent } from './app.component';
import { BooksComponent } from './books/books.component';  // Import BooksComponent

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent  // Declare the BooksComponent here
  ],
  imports: [
    BrowserModule,
    HttpClientModule,  // Add HttpClientModule here to make HTTP requests
    FormsModule  // Add FormsModule here to enable ngModel
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
