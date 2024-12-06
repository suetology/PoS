import { Component } from '@angular/core';
import { DiscountService } from '../../services/discount.service';
import { Observable } from 'rxjs';
import { Discount } from '../../types';
import { AsyncPipe, CommonModule, DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';
//import { AddDiscountComponent } from '../add-discount/add-discount.component';

@Component({
  selector: 'app-discount',
  standalone: true,
  imports: [CommonModule, AsyncPipe, DatePipe, RouterModule],
  templateUrl: './discount.component.html',
  styleUrl: './discount.component.css'
})
export class DiscountComponent {

  discounts$: Observable<Discount[]>;

  constructor(private discountService : DiscountService){
    this.discounts$ = this.discountService.getDiscounts();
  }
}
