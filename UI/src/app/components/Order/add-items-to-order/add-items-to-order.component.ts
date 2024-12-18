import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateOrderItemRequest, Item, ItemVariation } from '../../../types';
import { ItemService } from '../../../services/item.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-add-items-to-order',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule],
  templateUrl: './add-items-to-order.component.html',
  styleUrl: './add-items-to-order.component.css'
})
export class AddItemsToOrderComponent implements OnInit {
  @Output() orderItemsAdded = new EventEmitter<CreateOrderItemRequest[]>();

  addedOrderItems: CreateOrderItemRequest[] = [];
  addedOrderItemsDisplay: any = [];

  items: Item[] = [];
  itemVariations: ItemVariation[] = []

  itemVariationControls: { [key: string]: FormControl<boolean> } = {}

  itemForm = new FormGroup({
    itemId: new FormControl<string>('', Validators.required),
    quantity: new FormControl<number>(1, Validators.required)
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemService: ItemService) { }

  ngOnInit() {
    this.itemService.getItems().subscribe(
      (response) => {
        this.items = response;
      }
    );
  }

  onItemSelect() {
    const itemId = this.itemForm.value.itemId;
    
    if (!itemId) {
      return;
    }

    this.itemService.getAllItemVariations(itemId).subscribe(
      (response) => {
        this.itemVariations = response;

        this.itemVariationControls = {}
        this.itemVariations.forEach((variation) => {
          this.itemVariationControls[variation.id] = new FormControl<boolean>(false, { nonNullable: true });
        });
      }
    );
  }

  addItem() {
    if (!this.itemForm.value.itemId) {
      return;
    }

    const addedOrderItem: CreateOrderItemRequest = {
      itemId: this.itemForm.value.itemId || '',
      quantity: this.itemForm.value.quantity || 0,
      itemVariationsIds: this.itemVariations
        .map(variation => variation.id)
        .filter(variationId => this.itemVariationControls[variationId]?.value)
    }

    const currentItem = this.items.find(i => i.id === this.itemForm.value.itemId);
    const currentItemVariations = this.itemVariations.filter(variation => this.itemVariationControls[variation.id]?.value);

    const addedOrderItemDisplay = {
      name: currentItem?.name,
      quantity: this.itemForm.value.quantity,
      itemVariations: currentItemVariations.map(variation => {
        return {
          name: variation.name
        };
      })
    }
    
    this.addedOrderItems.push(addedOrderItem);
    this.addedOrderItemsDisplay.push(addedOrderItemDisplay);
  }

  close() {
    this.orderItemsAdded.emit(this.addedOrderItems);
  }
}
