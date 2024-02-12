import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}

  constructor(public accountService: AccountService) {
   }
  ngOnInit(): void {
  }

  login() {
    this.accountService.logn(this.model).subscribe({
      next: (response: any) => {
        console.log(response);
      },
      error: (error: any) => console.log(error)
    })
    console.log(this.model);
  }

  logout() {
    this.accountService.logout();
  }
}
