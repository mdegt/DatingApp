import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: Array<User> = [];

  constructor(private http : HttpClient) { }
  ngOnInit(): void {
    this.getUsers();
  }
  getUsers() {
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: (response: any) => this.users = response,
      error: (error: any) => console.log(error),
      complete: () => console.log('Request has completed.')
    });
  }


  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
