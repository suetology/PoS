import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DiscountService } from '../../services/discount.service';
import { HttpClient } from '@angular/common/http';
import { ItemService } from '../../services/item.service';
import { ItemGroupService } from '../../services/item-group.service';
import { AsyncPipe, NgFor } from '@angular/common';
import { Item, ItemGroup } from '../../types';

@Component({
  selector: 'app-add-discount',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgFor, AsyncPipe],
  templateUrl: './add-discount.component.html',
  styleUrl: './add-discount.component.css'
})
export class AddDiscountComponent {

  itemGroups: ItemGroup[] = []
  items: Item[] = []

  discountForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    value: new FormControl<number | null>(null, [Validators.required, Validators.min(0)]),
    isPercentage: new FormControl<boolean>(false, Validators.required),
    amountAvailable: new FormControl<number | null>(null, [Validators.required, Validators.min(0)]),
    validFrom: new FormControl<Date | null>(null, Validators.required),
    validTo: new FormControl<Date | null>(null, Validators.required),
    applicableItems: new FormControl<string[]>([]),
    applicableGroups: new FormControl<string[]>([])

  });

  constructor(private discountService: DiscountService,
    private itemService: ItemService,
    private itemGroupService: ItemGroupService, 
    private http : HttpClient) {}

  ngOnInit(): void {
    this.getItemGroups();
    this.getItems();
  }

  getItemGroups() {
    this.itemGroupService.getAllItemGroups().subscribe(
      (response) => {
        this.itemGroups = response;  
      }
    )
  }

  getItems() {
    this.itemService.getItems().subscribe(
      (response) => {
        this.items = response;  
      }
    )
  }

  onSubmit() {
    const discountRequest = {
      name: this.discountForm.value.name || '',
      value: this.discountForm.value.value ?? 0,
      isPercentage: this.discountForm.value.isPercentage ?? false,
      amountAvailable: this.discountForm.value.amountAvailable || 0,
      validFrom: this.discountForm.value.validFrom ? new Date(this.discountForm.value.validFrom).toISOString() : '',
      validTo: this.discountForm.value.validTo ? new Date(this.discountForm.value.validTo).toISOString() : '',
      applicableItems: this.discountForm.value.applicableItems || [],
      applicableGroups: this.discountForm.value.applicableGroups || []
    };

    this.discountService.addDiscount(discountRequest).subscribe({
      next: () => {
        console.log(discountRequest);
        this.discountForm.reset();
      },
      error: (err) => {
        console.error('Error adding discount:', err);
      }
    });
  }

}
