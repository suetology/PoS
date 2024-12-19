import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateCustomerRequest } from '../../../types';
import { AsyncPipe } from '@angular/common';
import { CustomerService } from '../../../services/customer.service';

@Component({
  selector: 'app-add-customer',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './add-customer.component.html',
  styleUrl: './add-customer.component.css'
})
export class AddCustomerComponent {
  customerForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    email: new FormControl<string>('', Validators.required),
    phoneNumber: new FormControl<string>('', Validators.required),
  });
  
  constructor(private customerService: CustomerService) { }

  onSubmit() {
    const createCustomerRequest: CreateCustomerRequest = {
      name: this.customerForm.value.name || '',
      email: this.customerForm.value.email || '',
      phoneNumber: this.customerForm.value.phoneNumber || '',
    }

    this.customerService.createCustomer(createCustomerRequest).subscribe({
      next: () => {
        this.customerForm.reset();
      },
      error: (err) => {
        console.error('Error adding customer: ', err);
      }
    });
  }
}
