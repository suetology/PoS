import { Component } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Observable, switchMap } from 'rxjs';
import { Business } from '../../../types';
import { BusinessService } from '../../../services/business.service';
import { Router, RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-business',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink, RouterModule],
  templateUrl: './business.component.html',
  styleUrl: './business.component.css'
})
export class BusinessComponent {

  business$: Observable<Business[]>;

  constructor(private businessService : BusinessService, private router: Router){
    this.business$ = this.businessService.getBusinessUpdated().pipe(
      switchMap(() => this.businessService.getBusiness())
    );
  }

  editBusiness(businessId: string) {
    this.router.navigate([`/business/${businessId}/edit`]);
  }
  
}

