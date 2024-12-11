import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ItemGroupService } from '../../../services/item-group.service';
import { HttpClient } from '@angular/common/http';
import { CreateItemGroupRequest } from '../../../types';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-add-item-group',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './add-item-group.component.html',
  styleUrl: './add-item-group.component.css'
})
export class AddItemGroupComponent{

  itemGroupForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    description: new FormControl<string>('', Validators.required),
  });

  constructor(private itemGroupService: ItemGroupService, private http : HttpClient) {}

  onSubmit() {
    const request: CreateItemGroupRequest = {
      name: this.itemGroupForm.value.name || '',
      description: this.itemGroupForm.value.description || ''
    }

    this.itemGroupService.createItemGroup(request).subscribe({
      next: () => {
        this.itemGroupForm.reset();
      },
      error: (err) => {
        console.error('Error adding item group:', err);
      }
    });
  }
}
