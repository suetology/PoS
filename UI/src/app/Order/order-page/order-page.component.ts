import { Component } from '@angular/core';
import { AddOrderComponent } from '../add-order/add-order.component';
import { OrderComponent } from '../order/order.component';

@Component({
  selector: 'app-order-page',
  standalone: true,
  imports: [OrderComponent, AddOrderComponent],
  templateUrl: './order-page.component.html',
  styleUrl: './order-page.component.css'
})
export class OrderPageComponent {

}
