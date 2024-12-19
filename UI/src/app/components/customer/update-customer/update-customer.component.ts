import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AsyncPipe, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomerService } from '../../../services/customer.service';

@Component({
  selector: 'app-update-customer',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, NgIf],
  templateUrl: './update-customer.component.html',
  styleUrl: './update-customer.component.css'
})
export class UpdateCustomerComponent {
  customerForm: FormGroup;
  customerId: string;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private customerService: CustomerService
  ) {
    this.customerId = this.route.snapshot.paramMap.get('id')!;
    this.customerForm = this.fb.group({
      name: [''],
      email: [''],
      phoneNumber: ['']
    });

    this.loadCustomer();
  }

  loadCustomer() {
    this.customerService.getCustomer(this.customerId).subscribe((customer) => {
      this.customerForm.patchValue({
        name: customer.name,
        email: customer.email,
        phoneNumber: customer.phoneNumber
      });
    });
  }

  onSubmit() {
    if (this.customerForm.invalid) {
      return;
    }

    const request = this.customerForm.value;
    this.customerService.updateCustomer(this.customerId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/customer']);
        },
        error: (error) => {
          console.error('Error updating customer:', error);
        },
      });
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  goBack() {
    this.router.navigate(['/customer']);
  }
}