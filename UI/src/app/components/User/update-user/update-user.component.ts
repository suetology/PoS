import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { User } from '../../../types';

@Component({
  selector: 'app-update-user',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.css'
})
export class UpdateUserComponent {
  userForm: FormGroup;
  userId: string;

  isOpen$ = new BehaviorSubject<boolean>(false);
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
      password: [''],
      phoneNumber: [''],
      email: ['', [Validators.email]],
      role: [''],
      status: ['']
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

  ngOnInit() {
    this.sub = this.isOpen$.subscribe((isOpen) => {
      if (isOpen) {
        const userId = this.route.snapshot.paramMap.get('id');
        if (userId) {
          this.userService.getUser(userId).subscribe((user) => (this.user = user));
        }
      }
    });
  }

  open(userId: string) {
    this.isOpen$.next(true);
  }

  close() {
    this.isOpen$.next(false);
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
