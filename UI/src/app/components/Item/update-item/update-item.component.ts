import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ItemService } from '../../../services/item.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemGroup, Tax } from '../../../types';
import { ItemGroupService } from '../../../services/item-group.service';
import { TaxService } from '../../../services/tax.service';
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-update-item',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf, NgFor],
  templateUrl: './update-item.component.html',
  styleUrl: './update-item.component.css'
})
export class UpdateItemComponent {

  itemForm: FormGroup;
  itemId: string;

  itemGroups: ItemGroup[] = [];
  taxes: Tax[] = [];

  private sub: Subscription | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private itemService: ItemService,
    private router: Router,
    private route: ActivatedRoute,
    private itemGroupService: ItemGroupService,
    private taxService: TaxService
  ) {
    this.itemId = this.route.snapshot.paramMap.get('id')!;
    this.itemForm = this.fb.group({
      name: [''],
      description: [''],
      price: [''],
      stock: [''],
      image: [''],
      itemGroupId: [''],
      taxIds: [[]]
    });

    this.loadItem();
  }

  ngOnInit(): void {
    this.getItemGroups();
    this.getTaxes();
    this.loadItem();
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

  loadItem() {
    this.itemService.getItem(this.itemId).subscribe((item) => {
      this.itemForm.patchValue({
        name: item.name,
        description: item.description,
        price: item.price,
        stock: item.stock,
        image: this.itemForm.value.image!,
        itemGroupId: item.itemGroupId || '',
        taxIds: item.taxIds || []
      });
    });
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
    if (this.itemForm.invalid) {
      return;
    }

    const request = this.itemForm.value;

    this.itemService.updateItem(this.itemId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/item']);
        },
        error: (error) => {
          console.error('Error updating item:', error);
        },
      });
  }

  goBack() {
    this.router.navigate(['/item']);
  }

  ngOnChanges() {
    if (this.itemId) {
      this.loadItem();
    }
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
