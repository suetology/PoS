import { Component } from '@angular/core';
import { ServiceChargeService } from '../../services/service-charge.service';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-service-charge',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './add-service-charge.component.html',
  styleUrl: './add-service-charge.component.css'
})
export class AddServiceChargeComponent {

  serviceChargeForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    description: new FormControl<string>('', Validators.required),
    value: new FormControl<number | null>(null, [Validators.required, Validators.min(0)]),
    isPercentage: new FormControl<boolean>(false, Validators.required)
  });

  constructor(private serviceChargeService: ServiceChargeService, private http : HttpClient) {}

   onSubmit() {
    const serviceChargeRequest = {
      name: this.serviceChargeForm.value.name || '',
      description: this.serviceChargeForm.value.description || '',
      value: this.serviceChargeForm.value.value ?? 0,
      isPercentage: this.serviceChargeForm.value.isPercentage ?? false
    }

    this.serviceChargeService.addServiceCharge(serviceChargeRequest).subscribe({
      next: () => {
        this.serviceChargeForm.reset();
      },
      error: (err) => {
        console.error('Error adding service charge:', err);
      }
    });
  }

}
