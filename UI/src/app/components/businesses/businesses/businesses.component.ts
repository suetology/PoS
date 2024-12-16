import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { Observable, Subscription, switchMap } from 'rxjs';
import { Business, CreateBusinessRequest } from '../../../types';
import { BusinessService } from '../../../services/business.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-businesses',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink, RouterModule, FormsModule, ReactiveFormsModule],
  templateUrl: './businesses.component.html',
  styleUrl: './businesses.component.css'
})
export class BusinessesComponent {
  private updateSub: Subscription;

  businesses$: Observable<Business[]>;

  businessForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    address: new FormControl<string>('', Validators.required),
    phoneNumber: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', Validators.required)
  });

  constructor(
    private businessService: BusinessService,
    private userService: UserService,
    private authService: AuthService,
    private router: Router){
    
    this.businesses$ = this.businessService.getBusiness();

    this.updateSub = this.businessService.getBusinessUpdated().subscribe(() => {
      this.businesses$ = this.businessService.getBusiness();
    });
  }

  logInto(businessId: string) {
    const userId = this.authService.getUserId() || '';

    this.userService.setBusiness(userId, businessId).subscribe(() => {
      this.authService.logout();
      this.router.navigate(['/login']);
    });
  }

  createBusiness() {
    const request: CreateBusinessRequest = {
      name: this.businessForm.value.name || '',
      address: this.businessForm.value.address || '',
      phoneNumber: this.businessForm.value.phoneNumber || '',
      email: this.businessForm.value.email || ''
    };

    this.businessService.createBusiness(request).subscribe({
      next: () => {
        this.businessForm.reset();
      },
      error: (err) => {
        console.error('Error creating business:', err);
      }
    });
  }
}
