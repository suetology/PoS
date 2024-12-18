import { AsyncPipe, DatePipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Order, DiscountRequest, AddTipRequest, UpdateOrderRequest, CreateOrderItemRequest, AddItemInOrderRequest, PaymentMethod, OrderStatus, CreateCardPaymentRequest, CreateCashOrGiftCardPaymentRequest, CreateRefundRequest } from '../../../types';
import { OrderService } from '../../../services/order.service';
import { DiscountService } from '../../../services/discount.service';
import { AddItemsToOrderComponent } from '../add-items-to-order/add-items-to-order.component';
import { PaymentService } from '../../../services/payment.service';
import { RefundService } from '../../../services/refund.service';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule, DatePipe, AddItemsToOrderComponent],
  templateUrl: './order-details.component.html',
  styleUrl: './order-details.component.css'
})
export class OrderDetailsComponent {

  isAddItemModalOpen = false;

  openAddItemModal() {
    this.isAddItemModalOpen = true;
  }

  closeAddItemModal() {
    this.isAddItemModalOpen = false;
  }

  order: Order | undefined;

  discountForm = new FormGroup({
    discountName: new FormControl<string>('', Validators.required),
    discountValue: new FormControl<number>(0, Validators.required),
    discountIsPercentage: new FormControl<boolean>(false, Validators.required),
  });

  tipForm = new FormGroup({
    tipAmount: new FormControl<number>(0, Validators.required)
  });

  paymentForm = new FormGroup({
    paymentAmount: new FormControl<number>(0, Validators.required),
    paymentMethod: new FormControl<number>(0, Validators.required),
  });

  refundForm = new FormGroup({
    refundReason: new FormControl<string>('', Validators.required)
  })

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private orderService: OrderService,
    private discountService: DiscountService,
    private paymentService: PaymentService,
    private refundService: RefundService
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
    this.loadOrderDetails();
  }
  
  private loadOrderDetails() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.close();
      return;
    }

    this.orderService.getOrder(id).subscribe({
      next: (order) => (this.order = order),
      error: (err) => {
        console.error('Error fetching order details:', err);
        this.close();
      }
    });
  }

  handleOrderItemsAdded(orderItems: CreateOrderItemRequest[]) {
    const orderId = this.route.snapshot.paramMap.get('id');
    if (!orderId || orderItems.length === 0) return;
  
    const requests: AddItemInOrderRequest[] = [];
    
    orderItems.forEach((orderItem) => {
      const existingOrderItem = this.order?.orderItems.find(
        (oi) =>
          oi.item.id === orderItem.itemId &&
          this.areItemVariationsEqual(oi.itemVariations, orderItem.itemVariationsIds)
      );
  
      if (existingOrderItem) {
        const updatedQuantity = existingOrderItem.quantity + orderItem.quantity;
        this.updateOrderItem(orderItem.itemId, updatedQuantity, orderItem.itemVariationsIds);
      } else {
        const request: AddItemInOrderRequest = {
          itemId: orderItem.itemId,
          quantity: orderItem.quantity,
          itemVariationsIds: orderItem.itemVariationsIds || []
        };
  
        this.orderService.addItemToOrder(orderId, request).subscribe({
          next: () => this.loadOrderDetails(),
          error: (err) => console.error('Error adding item:', err)
        });
      }
    });
  }  
  
  areItemVariationsEqual(
    existingVariations: { id: string }[],
    newVariations: string[]
  ): boolean {
    if (!existingVariations || !newVariations) return false;
    if (existingVariations.length !== newVariations.length) return false;
  
    const existingIds = existingVariations.map((v) => v.id).sort();
    const newIds = newVariations.sort();
  
    return JSON.stringify(existingIds) === JSON.stringify(newIds);
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

  pay() {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      return;
    }

    const paymentAmount = this.paymentForm.value.paymentAmount || 0;
    const paymentMethod = (this.paymentForm.value.paymentMethod as PaymentMethod) || 0;

    if (paymentMethod == PaymentMethod.Cash || paymentMethod == PaymentMethod.GiftCard) {
      const request: CreateCashOrGiftCardPaymentRequest = {
        paymentAmount: paymentAmount,
        paymentMethod: paymentMethod,
        orderId: id
      };

      this.paymentService.createCashOrGiftCardPayment(request).subscribe({
        next: () => {
          this.orderService.getOrder(id).subscribe({
            next: (order) => this.order = order
          });
        }
      })
    } else {
      const request: CreateCardPaymentRequest = {
        paymentAmount: paymentAmount,
        orderId: id
      };

      this.paymentService.createCardPayment(request).subscribe({
        next: (sessionUrl) => {
          console.log(sessionUrl);
          if (sessionUrl) {
            window.location.href = sessionUrl;
          }
        }
      });
    }
  }

  refund() {
    const id = this.route.snapshot.paramMap.get('id');

    if (!id) {
      return;
    }

    const request: CreateRefundRequest = {
      orderId: id,
      reason: this.refundForm.value.refundReason || ''
    };

    this.refundService.createRefund(request).subscribe({
      next: () => {
        this.orderService.getOrder(id).subscribe({
          next: (order) => this.order = order
        });
      }
    });
  }

  isOrderOpen() {
    return this.order?.status.toString() == OrderStatus[OrderStatus.Open];
  }

  isOrderPartiallyPaid() {
    return this.order?.status.toString() == OrderStatus[OrderStatus.PartiallyPaid];
  }

  isOrderClosed() {
    return this.order?.status.toString() == OrderStatus[OrderStatus.Closed];
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

  deleteOrderItem(orderId: string, itemId: string) {

    this.orderService.deleteItem(orderId, itemId).subscribe({
      next: () => this.loadOrderDetails(),
      error: (err) => console.error('Error deleting item:', err)
    })
  }
}
