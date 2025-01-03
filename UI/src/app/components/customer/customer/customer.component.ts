import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter, Observable, Subscription } from 'rxjs';
import { Customer, User } from '../../../types';
import { AsyncPipe, CommonModule } from '@angular/common';
import { CustomerService } from '../../../services/customer.service';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
  
  customers$: Observable<Customer[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private customerService : CustomerService, 
    private router: Router,
    private route: ActivatedRoute){

    this.customers$ = this.customerService.getAllCustomers();

    this.updateSub = this.customerService.getCustomersUpdated().subscribe(() => {
      this.customers$ = this.customerService.getAllCustomers();
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

  trackById(index: number, customer: Customer): string {
    return customer.id;
  }

  goToCustomerDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editCustomer(customerId: string) {
    this.router.navigate([`/customer/${customerId}/edit`]);
  }

  retireCustomer(customerId: string) {
    this.customerService.retireCustomer(customerId).subscribe({
      next: () => {
        this.router.navigate(['/customer']);
      },
      error: (error) => {
        console.error('Error retiring customer:', error);
      },
    });
  }
}
