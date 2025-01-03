import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  loginError: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onLogin() {
    this.authService.login(this.username, this.password).subscribe({
      next: (success) => {
        if (success) {
          console.log('Login successful', success);
          if (this.authService.getRole() === "SuperAdmin") {
            this.router.navigate(['/businesses']);
          } else {
            this.router.navigate(['/business']);
          }
        } else {
          this.loginError = 'Invalid username or password';
        }
      },
    });  
  }
}
