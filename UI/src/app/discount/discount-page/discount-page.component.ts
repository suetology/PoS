import { Component } from '@angular/core';
import { DiscountComponent } from '../discount/discount.component';
import { AddDiscountComponent } from '../add-discount/add-discount.component';

@Component({
  selector: 'app-discount-page',
  standalone: true,
  imports: [DiscountComponent, AddDiscountComponent],
  templateUrl: './discount-page.component.html',
  styleUrl: './discount-page.component.css'
})
export class DiscountPageComponent {

}
