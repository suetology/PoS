import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { CreateItemVariationRequest, Item, ItemVariation } from '../../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemService } from '../../../services/item.service';
import { ItemGroupService } from '../../../services/item-group.service';
import { TaxService } from '../../../services/tax.service';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-item-details',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule],
  templateUrl: './item-details.component.html',
  styleUrl: './item-details.component.css'
})
export class ItemDetailsComponent {

  item: Item | undefined;
  itemGroupName: string | undefined;
  taxNames: string[] = [];
  itemVariations: ItemVariation[] = [];

  itemVariationForm = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    description: new FormControl<string>('', [Validators.required]),
    addedPrice: new FormControl<number>(0, [Validators.required]),
    stock: new FormControl<number>(0, [Validators.required])
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private itemService: ItemService,
    private itemGroupService: ItemGroupService,
    private taxService: TaxService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.itemService.getItem(id).subscribe(
        (item) => {
          this.item = item;

          if (item.itemGroupId) {
            this.itemGroupService.getItemGroup(item.itemGroupId).subscribe({
              next: (itemGroup) => {
                this.itemGroupName = itemGroup.name;
              }
            });
          }

          if (item.taxIds.length !== 0) {       
          for (const taxId of item.taxIds) {
            this.taxService.getTax(taxId).subscribe(
              (tax) => {
                this.taxNames.push(tax.name);
              }
            );
          }
        }

          this.itemService.getAllItemVariations(item.id).subscribe(
            (itemVariations) => {
              this.itemVariations = itemVariations;
            }
          );
        },
        (error) => {
          console.error('Error fetching item details:', error);
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

  addItemVariation() {
    if (!this.itemVariationForm.valid) {
      return;
    }

    if (!this.item) {
      return;
    }

    const request: CreateItemVariationRequest = {
      name: this.itemVariationForm.value.name || '',
      description: this.itemVariationForm.value.description || '',
      addedPrice: this.itemVariationForm.value.addedPrice || 0,
      stock: this.itemVariationForm.value.stock || 0
    };

    this.itemService.createItemVariation(this.item.id, request).subscribe({
      next: () => {
        this.itemVariationForm.reset();

        if (!this.item) {
          return;
        }

        this.itemService.getAllItemVariations(this.item.id).subscribe(
          (itemVariations) => {
            this.itemVariations = itemVariations;
          }
        )
      },
      error: (err) => {
        console.log("Error creating item variation: ", err);
      }
    })
  }
}
