import { AsyncPipe, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CreateItemRequest, Item, ItemGroup, Tax } from '../../../types';
import { ItemService } from '../../../services/item.service';
import { TaxService } from '../../../services/tax.service';
import { ItemGroupService } from '../../../services/item-group.service';

@Component({
  selector: 'app-add-item',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, NgFor],
  templateUrl: './add-item.component.html',
  styleUrl: './add-item.component.css'
})
export class AddItemComponent {
  itemGroups: ItemGroup[] = []
  taxes: Tax[] = []
  
  itemForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    description: new FormControl<string>('', Validators.required),
    price: new FormControl<number>(0, Validators.required),
    stock: new FormControl<number>(0, Validators.required),
    image: new FormControl<string>('', Validators.required),
    itemGroupId: new FormControl<string>(''),
    taxIds: new FormControl<string[]>([])
  });
  
  constructor(
    private itemService: ItemService,
    private itemGroupService: ItemGroupService,
    private taxService: TaxService) {}
  
  ngOnInit(): void {
    this.getItemGroups();
    this.getTaxes();
  }

  getItemGroups() {
    this.itemGroupService.getAllItemGroups().subscribe(
      (response) => {
        this.itemGroups = response;  
      }
    )
  }

  getTaxes() {
    this.taxService.getTaxes().subscribe(
      (response) => {
        this.taxes = response;  
      }
    )
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;
  
    const file = input.files[0];
    const reader = new FileReader();
  
    reader.onload = (e: any) => {
      const base64Only = e.target.result.split(',')[1];
      this.itemForm.patchValue({ image: base64Only });
    };
    reader.readAsDataURL(file);
  }

  onSubmit() {
    const request: CreateItemRequest = {
      name: this.itemForm.value.name || '',
      description: this.itemForm.value.description || '',
      price: this.itemForm.value.price || 0,
      stock: this.itemForm.value.stock || 0,
      image: this.itemForm.value.image!,
      itemGroupId: this.itemForm.value.itemGroupId || undefined,
      taxIds: this.itemForm.value.taxIds || []
    }

    this.itemService.createItem(request).subscribe({
      next: () => {
        this.itemForm.reset();
      },
      error: (err) => {
        console.error('Error creating item:', err);
      }
    });
  }
}
