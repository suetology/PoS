import { Component } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { map, Observable, switchMap } from 'rxjs';
import { Business } from '../../../types';
import { BusinessService } from '../../../services/business.service';
import { Router, RouterLink, RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-business',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink, RouterModule],
  templateUrl: './business.component.html',
  styleUrl: './business.component.css'
})
export class BusinessComponent {

  business: Business | undefined;

  constructor(
    private businessService: BusinessService, 
    private authService: AuthService,
    private router: Router){
      const id = authService.getBusinessId() || '';
        this.businessService.getBusinessById(id).subscribe(
          (response) => this.business = response
        );
  }

  editBusiness(businessId: string) {
    this.router.navigate([`/business/${businessId}/edit`]);
  }
  
}

