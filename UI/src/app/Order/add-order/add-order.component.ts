import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { OrderService } from '../../services/order.service';
import { CreateCustomerRequest, CreateOrderItemRequest, CreateOrderRequest, CreateReservationRequest, Customer, Item, ItemVariation, ServiceCharge } from '../../types';
import { ServiceChargeService } from '../../services/service-charge.service';
import { CustomerService } from '../../services/customer.service';
import { AddItemsToOrderComponent } from '../add-items-to-order/add-items-to-order.component';
import { AddReservationToOrderComponent } from '../add-reservation-to-order/add-reservation-to-order.component';

@Component({
  selector: 'app-add-order',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgFor, NgIf, AsyncPipe, AddItemsToOrderComponent, AddReservationToOrderComponent],
  templateUrl: './add-order.component.html',
  styleUrl: './add-order.component.css'
})
export class AddOrderComponent {
  serviceCharges: ServiceCharge[] = [];
  customers: Customer[] = [];
  isExistingCustomer: boolean = false;

  isItemsModalOpen: boolean = false;
  isReservationModalOpen: boolean = false;

  addedOrderItems: CreateOrderItemRequest[] = [];
  addedReservation: CreateReservationRequest | undefined = undefined;

  orderForm = new FormGroup({
    isExistingCustomer: new FormControl<boolean>(false, Validators.required),
    customerId: new FormControl<string>('', Validators.required),
    customerName: new FormControl<string>('', Validators.required),
    customerEmail: new FormControl<string>('', Validators.required),
    customerPhoneNumber: new FormControl<string>('', Validators.required),
    serviceChargeId: new FormControl<string>('', Validators.required)
  });

  constructor(
    private serviceChargeService: ServiceChargeService,
    private customerService: CustomerService,
    private orderService: OrderService) { }

  ngOnInit() {
    this.getServiceCharges();
    this.getCustomers();
  }

  getServiceCharges() {
    this.serviceChargeService.getServiceCharges().subscribe(
      (response) => {
        this.serviceCharges = response;  
      }
    );
  }

  getCustomers() {
    this.customerService.getAllCustomers().subscribe(
      (response) => {
        this.customers = response;  
      }
    );
  }

  toggleExistingCustomer() {
    this.isExistingCustomer = !this.isExistingCustomer;
  }

  openAddItemsModal() {
    this.isItemsModalOpen = true;
  }

  openAddReservationModal() {
    this.isReservationModalOpen = true;
  }

  onOrderItemsAdded(addedOrderItems: CreateOrderItemRequest[]) {
    this.isItemsModalOpen = false;

    this.addedOrderItems = addedOrderItems;
  }

  onReservationAdded(addedReservation: CreateReservationRequest | undefined) {
    this.isReservationModalOpen = false;

    this.addedReservation = addedReservation;
  }

  onSubmit() {
    const customer: CreateCustomerRequest = {
      name: this.orderForm.value.customerName || '',
      email: this.orderForm.value.customerEmail || '',
      phoneNumber: this.orderForm.value.customerPhoneNumber || ''
    };

    const request: CreateOrderRequest = {
      customerId: this.isExistingCustomer ? this.orderForm.value.customerId || undefined : undefined,
      customer: this.isExistingCustomer ? undefined : customer,
      serviceChargeId: this.orderForm.value.serviceChargeId || undefined,
      orderItems: this.addedOrderItems,
      reservation: this.addedReservation
    };

    this.orderService.createOrder(request).subscribe({
      next: () => {
        this.orderForm.reset();
        this.addedReservation = undefined;
        this.addedOrderItems = [];
      },
      error: (err) => {
        console.error('Error creating order:', err);
      }
    });
  }
}
