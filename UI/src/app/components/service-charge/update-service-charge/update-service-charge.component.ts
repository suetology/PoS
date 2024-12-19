import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AsyncPipe, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ServiceChargeService } from '../../../services/service-charge.service';

@Component({
  selector: 'app-update-service-charge',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, NgIf],
  templateUrl: './update-service-charge.component.html',
  styleUrl: './update-service-charge.component.css'
})
export class UpdateServiceChargeComponent {
  serviceChargeForm: FormGroup;
  serviceChargeId: string;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private serviceChargeService: ServiceChargeService
  ) {
    this.serviceChargeId = this.route.snapshot.paramMap.get('id')!;
    this.serviceChargeForm = this.fb.group({
      name: [''],
      description: [''],
      value: [''],
      isPercentage: ['']
    });

    this.loadServiceCharge();
  }

  loadServiceCharge() {
    this.serviceChargeService.getServiceCharge(this.serviceChargeId).subscribe((serviceCharge) => {
      this.serviceChargeForm.patchValue({
        name: serviceCharge.name,
        description: serviceCharge.description,
        value: serviceCharge.value,
        isPercentage: serviceCharge.isPercentage
      });
    });
  }

  onSubmit() {
    if (this.serviceChargeForm.invalid) {
      return;
    }

    const request = this.serviceChargeForm.value;
    this.serviceChargeService.updateServiceCharge(this.serviceChargeId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/service-charge']);
        },
        error: (error) => {
          console.error('Error updating service charge:', error);
        },
      });
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  goBack() {
    this.router.navigate(['/service-charge']);
  }
}