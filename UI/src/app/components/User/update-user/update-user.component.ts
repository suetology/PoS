import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { User } from '../../../types';

@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.css'
})
export class UpdateUserComponent {

  userId: string;
  userForm: FormGroup;

  user: User | undefined;
  private sub: Subscription | undefined = undefined;
  
  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.userId = this.route.snapshot.paramMap.get('id')!;
    this.userForm = this.fb.group({
      name: ['', Validators.required],
      surname: [''],
      username: [''],
      passwordHash: [''],
      phoneNumber: [''],
      email: [''],
      role: [''],
    });

    this.loadUser();
  }

  loadUser() {
    this.userService.getUser(this.userId).subscribe((user) => {
      this.userForm.patchValue(user);
    });
  }

  onSubmit() {
    if (this.userForm.invalid) {
      return;
    }

    this.userService.updateUser(this.userId, this.userForm.value)
      .subscribe({
        next: () => {
          this.router.navigate(['/user']);
        },
        error: (error) => {
          console.error('Error updating user:', error);
        },
      });
  }

  goBack() {
    this.router.navigate(['/user']);
  }

  ngOnChanges() {
    if (this.userId) {
      this.loadUser();
    }
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
