import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  model: any = {}

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }
  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
       },
      error: (error:any) =>  console.log(error)
    })
    console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false);
    console.log('Cancelled!');
  }
}
