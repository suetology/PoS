import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  isLoggedIn: boolean = false;
  username: string = '';

  constructor(private authService: AuthService) {
    this.authService.isLoggedIn$.subscribe((status) => this.isLoggedIn = status);
    this.authService.username$.subscribe((name) => this.username = name);
  }

  onLogout() {
    this.authService.logout();
  }
}
