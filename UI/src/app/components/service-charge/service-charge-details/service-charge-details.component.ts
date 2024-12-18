import { Component, OnInit } from '@angular/core';
import { ServiceCharge, Tax } from '../../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { AsyncPipe, NgIf } from '@angular/common';
import { ServiceChargeService } from '../../../services/service-charge.service';

@Component({
  selector: 'app-service-charge-details',
  standalone: true,
  imports: [AsyncPipe, NgIf],
  templateUrl: './service-charge-details.component.html',
  styleUrl: './service-charge-details.component.css'
})
export class ServiceChargeDetailsComponent implements OnInit{

  serviceCharge: ServiceCharge | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceChargeService: ServiceChargeService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.serviceChargeService.getServiceCharge(id).subscribe(
        (serviceCharge) => {
          this.serviceCharge = serviceCharge;
        },
        (error) => {
          console.error('Error fetching service charge details:', error);
          this.close();
        }
      );
    } else {
      this.close();
    }
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}