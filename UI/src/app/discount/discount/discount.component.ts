import { Component } from '@angular/core';
import { DiscountService } from '../../services/discount.service';
import { filter, Observable, Subject, switchMap, takeUntil } from 'rxjs';
import { Discount } from '../../types';
import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-discount',
  standalone: true,
  imports: [CommonModule, AsyncPipe, DatePipe, RouterModule, RouterOutlet],
  templateUrl: './discount.component.html',
  styleUrl: './discount.component.css'
})
export class DiscountComponent {

  discounts$: Observable<Discount[]>;
  isModalOpen = false;

  private destroy$ = new Subject<void>();
  
  constructor(private discountService : DiscountService,
    private router: Router,
    private route: ActivatedRoute
  )
  {
    this.discounts$ = this.discountService.getDiscountsUpdated().pipe(
      switchMap(() => this.discountService.getDiscounts())
    );

    // Handle modal visibility
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        takeUntil(this.destroy$)
      )
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  trackById(index: number, discount: Discount): string {
    return discount.id;
  }

  goToDiscountDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }
}
