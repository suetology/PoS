import { AsyncPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Order, DiscountRequest, AddTipRequest, UpdateOrderRequest } from '../../../types';
import { OrderService } from '../../../services/order.service';
import { DiscountService } from '../../../services/discount.service';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule, DatePipe],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent {
  order: Order | undefined;

  discountForm = new FormGroup({
    discountName: new FormControl<string>('', Validators.required),
    discountValue: new FormControl<number>(0, Validators.required),
    discountIsPercentage: new FormControl<boolean>(false, Validators.required),
  });

  tipForm = new FormGroup({
    tipAmount: new FormControl<number>(0, Validators.required)
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService,
    private discountService: DiscountService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.orderService.getOrder(id).subscribe(
        (order) => {
          this.order = order;
        },
        (error) => {
          console.error('Error fetching order details:', error);
          this.close();
        }
      );
    } else {
      this.close();
    }
  }

  addDiscount() {
    const request: DiscountRequest = {
      name: this.discountForm.value.discountName || '',
      value: this.discountForm.value.discountValue || 0,
      isPercentage: this.discountForm.value.discountIsPercentage || false,
      applicableOrder: this.route.snapshot.paramMap.get('id') || '',
    };

    this.discountService.addDiscount(request).subscribe({
      next: () => {
        this.discountForm.reset();

        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
          this.orderService.getOrder(id).subscribe(
            (order) => {
              this.order = order;

            },
            (error) => {
              console.error('Error fetching order details:', error);
              this.close();
            }
          );
        }
      },
      error: (err) => {
        console.error('Error creating discount:', err);
      }
    });
  }

  addTip() {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      return;
    }

    const request: AddTipRequest = {
      tipAmount: this.tipForm.value.tipAmount || 0
    };

    this.orderService.addTip(id, request).subscribe({
      next: () => {
        this.tipForm.reset();

        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
          this.orderService.getOrder(id).subscribe(
            (order) => {
              this.order = order;

            },
            (error) => {
              console.error('Error fetching order details:', error);
              this.close();
            }
          );
        }
      },
      error: (err) => {
        console.error('Error creating discount:', err);
      }
    });
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  onPlus(orderItem: any) {
    const updatedQuantity = orderItem.quantity + 1;
  
    this.updateOrderItem(orderItem.item.id, updatedQuantity, orderItem.itemVariations.map((v: any) => v.id));
  }

  onMinus(orderItem: any) {
    if (orderItem.quantity <= 1) return;
  
    const updatedQuantity = orderItem.quantity - 1;
  
    this.updateOrderItem(orderItem.item.id, updatedQuantity, orderItem.itemVariations.map((v: any) => v.id));
  }

  updateOrderItem(itemId: string, quantity: number, itemVariationsIds: string[]) {
    
    const id = this.route.snapshot.paramMap.get('id');
    if (!id || !this.order) return;
  
    const updatedItems = this.order.orderItems.map(orderItem => 
      orderItem.item.id === itemId 
        ? { ...orderItem, quantity, itemVariations: orderItem.itemVariations }
        : orderItem
    );

    const request: UpdateOrderRequest = {
      orderItems: updatedItems.map(orderItem => ({
        itemId: orderItem.item.id,
        itemVariationsIds: orderItem.itemVariations.map(v => v.id),
        quantity: orderItem.quantity
      }))
    };
  
    this.orderService.updateOrder(id, request).subscribe({
      next: () => {
        this.orderService.getOrder(id).subscribe((order) => {
          this.order = order;
        });
      },
      error: (err) => console.error('Error updating order:', err)
    });
  }

  // deleteOrderItem(orderId: string, itemId: string) {
  // }
  
}
