import { Component } from '@angular/core';
import { Discount, Item } from '../../types';
import { DiscountService } from '../../services/discount.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DatePipe, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-discount-details',
  standalone: true,
  imports: [DatePipe, NgFor, NgIf],
  templateUrl: './discount-details.component.html',
  styleUrl: './discount-details.component.css'
})
export class DiscountDetailsComponent {

  discount: Discount | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private discountService: DiscountService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.discountService.getDiscount(id).subscribe(
        (discount) => {
          this.discount = discount;
        },
        () => {
          this.close();
        }
      );
    } else {
      this.close();
    }
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  trackById(index: number, item: Item): string {
    return item.id;
  }

  onDelete(discountId:string){
    if (!this.discount) return;

    this.discountService.deleteDiscount(discountId).subscribe({
      next: () => {
          alert('Discount deleted');

          this.discountService.triggerDiscountsUpdated();  
          this.close();
      }
    });
  }
}
