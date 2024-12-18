import { Component } from '@angular/core';
import { filter, Observable, Subscription } from 'rxjs';
import { ServiceCharge } from '../../../types';
import { ServiceChargeService } from '../../../services/service-charge.service';
import { AsyncPipe } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-service-charge',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './service-charge.component.html',
  styleUrl: './service-charge.component.css'
})
export class ServiceChargeComponent {

  serviceCharges$: Observable<ServiceCharge[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private serviceChargeService : ServiceChargeService, 
    private router: Router, private route: ActivatedRoute) {

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

  trackById(index: number, serviceCharge: ServiceCharge): string {
    return serviceCharge.id;
  }

  goToServiceChargeDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editServiceCharge(serviceChargeId: string) {
    this.router.navigate([`/serviceCharge/${serviceChargeId}/edit`]);
  }
}
