import { Component } from '@angular/core';
import { ServiceCharge, Tax } from '../../../types';
import { filter, Observable, Subscription } from 'rxjs';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { RouterOutlet } from '@angular/router';
import { ServiceChargeService } from '../../../services/service-charge.service';

@Component({
  selector: 'app-service-charge',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './service-charge.component.html',
  styleUrl: './service-charge.component.css'
})
export class ServiceChargeComponent {
  
  serviceCharges$: Observable<ServiceCharge[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private serviceChargeService : ServiceChargeService, 
    private router: Router,
    private route: ActivatedRoute){

    this.serviceCharges$ = this.serviceChargeService.getServiceCharges();

    this.updateSub = this.serviceChargeService.getserviceChargesUpdated().subscribe(() => {
      this.serviceCharges$ = this.serviceChargeService.getServiceCharges();
    });

    this.routeSub = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, serviceCharge: Tax): string {
    return serviceCharge.id;
  }

  goToServiceChargeDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editServiceCharge(serviceChargeId: string) {
    this.router.navigate([`/service-charge/${serviceChargeId}/edit`]);
  }

  retireServiceCharge(taxId: string) {
    this.serviceChargeService.retireServiceCharge(taxId).subscribe({
      next: () => {
        this.router.navigate(['/service-charge']);
      },
      error: (error) => {
        console.error('Error retiring service charge:', error);
      },
    });
}
}

