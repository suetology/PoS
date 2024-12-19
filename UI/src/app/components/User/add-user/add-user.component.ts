import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { CreateUserRequest, Role } from '../../../types';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent {
  userForm = new FormGroup({
    username: new FormControl<string>('', Validators.required),
    passwordHash: new FormControl<string>('', Validators.required),
    name: new FormControl<string>('', Validators.required),
    surname: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', Validators.required),
    phoneNumber: new FormControl<string>('', Validators.required),
    role: new FormControl<string>('', Validators.required),
  });
  
  constructor(private userService: UserService) { }

  onSubmit() {
    const createUserRequest: CreateUserRequest = {
      username: this.userForm.value.username || '',
      passwordHash: this.userForm.value.passwordHash || '',
      name: this.userForm.value.name || '',
      surname: this.userForm.value.surname || '',
      email: this.userForm.value.email || '',
      phoneNumber: this.userForm.value.phoneNumber || '',
      role: (this.userForm.value.role as Role) || '',
    }

    this.userService.addUser(createUserRequest).subscribe({
      next: () => {
        this.userForm.reset();
      },
      error: (err) => {
        console.error('Error adding user: ', err);
      }
    });
  }
}
