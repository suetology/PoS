import { Component } from '@angular/core';
import { AddItemGroupComponent } from '../add-item-group/add-item-group.component';
import { ItemGroupComponent } from '../item-group/item-group.component';

@Component({
  selector: 'app-item-group-page',
  standalone: true,
  imports: [AddItemGroupComponent, ItemGroupComponent],
  templateUrl: './item-group-page.component.html',
  styleUrl: './item-group-page.component.css'
})
export class ItemGroupPageComponent {

}
