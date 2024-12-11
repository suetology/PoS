import { Component } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { ServiceCharge } from '../../../types';
import { ServiceChargeService } from '../../../services/service-charge.service';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-service-charge-list',
  standalone: true,
  imports: [AsyncPipe],
  templateUrl: './service-charge-list.component.html',
  styleUrl: './service-charge-list.component.css'
})
export class ServiceChargeListComponent {

  serviceCharges$: Observable<ServiceCharge[]>;
  private updateSub: Subscription;

  constructor(private serviceChargeService : ServiceChargeService){

    this.serviceCharges$ = this.serviceChargeService.getServiceCharges();

    this.updateSub = this.serviceChargeService.getserviceChargesUpdated().subscribe(() => {
      this.serviceCharges$ = this.serviceChargeService.getServiceCharges();
    });
  }

  ngOnDestroy() {
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, serviceCharge: ServiceCharge): string {
    return serviceCharge.id;
  }
}
