import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { DiscountRequest, Order } from '../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../services/order.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DiscountService } from '../../services/discount.service';

@Component({
  selector: 'app-order-details',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule],
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

          
          console.log(order);
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

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
