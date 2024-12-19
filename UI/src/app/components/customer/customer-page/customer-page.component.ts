import { Component } from '@angular/core';
import { AddCustomerComponent } from '../add-customer/add-customer.component';
import { CustomerComponent } from '../customer/customer.component';

@Component({
  selector: 'app-customer-page',
  standalone: true,
  imports: [AddCustomerComponent, CustomerComponent],
  templateUrl: './customer-page.component.html',
  styleUrl: './customer-page.component.css'
})
export class CustomerPageComponent {
}
