import { Component } from '@angular/core';
import { AddItemComponent } from '../add-item/add-item.component';
import { ItemComponent } from '../item/item.component';

@Component({
  selector: 'app-item-page',
  standalone: true,
  imports: [AddItemComponent, ItemComponent],
  templateUrl: './item-page.component.html',
  styleUrl: './item-page.component.css'
})
export class ItemPageComponent {

}
