import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ItemGroupService } from '../../../services/item-group.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-update-item-group',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './update-item-group.component.html',
  styleUrl: './update-item-group.component.css'
})
export class UpdateItemGroupComponent {
  itemGroupForm: FormGroup;
  itemGroupId: string;

  private sub: Subscription | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private itemGroupService: ItemGroupService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.itemGroupId = this.route.snapshot.paramMap.get('id')!;
    this.itemGroupForm = this.fb.group({
      name: [''],
      description: ['']
    });

    this.loadItemGroup();
  }

  loadItemGroup() {
    this.itemGroupService.getItemGroup(this.itemGroupId).subscribe((itemGroup) => {
      this.itemGroupForm.patchValue(itemGroup);
    });
  }

  onSubmit() {
    if (this.itemGroupForm.invalid) {
      return;
    }

    this.itemGroupService.updateItemGroup(this.itemGroupId, this.itemGroupForm.value)
      .subscribe({
        next: () => {
          this.router.navigate(['/item-group']);
        },
        error: (error) => {
          console.error('Error updating item group:', error);
        },
      });
  }

  goBack() {
    this.router.navigate(['/item-group']);
  }

  ngOnChanges() {
    if (this.itemGroupId) {
      this.loadItemGroup();
    }
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}
