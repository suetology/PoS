import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter, Observable, Subscription } from 'rxjs';
import { Order, OrderStatus } from '../../../types';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent {
  
  orders$: Observable<Order[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private orderService : OrderService, 
    private router: Router,
    private route: ActivatedRoute) {

    this.orders$ = this.orderService.getOrders();

    this.updateSub = this.orderService.getOrdersUpdated().subscribe(() => {
      this.orders$ = this.orderService.getOrders();
    });

    this.routeSub = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, order: Order): string {
    return order.id;
  }

  goToOrderDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  cancelOrder(id: string) {
    this.orderService.cancelOrder(id).subscribe(() => {
      this.orders$ = this.orderService.getOrders();
    });
  }

  getStatusLabel(status: string | OrderStatus): string {
    return typeof status === 'string' ? status : OrderStatus[status];
  }  
}
