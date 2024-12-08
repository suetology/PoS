import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TaxService } from '../../services/tax.service';
import { HttpClient } from '@angular/common/http';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-add-tax',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe],
  templateUrl: './add-tax.component.html',
  styleUrl: './add-tax.component.css'
})
export class AddTaxComponent{

  taxForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    type: new FormControl<string>('', Validators.required),
    value: new FormControl<number | null>(null, [Validators.required, Validators.min(0)]),
    isPercentage: new FormControl<boolean>(false, Validators.required)
  });

  constructor(private taxService: TaxService, private http : HttpClient) {}

  onSubmit() {
    const taxRequest = {
      name: this.taxForm.value.name || '',
      type: +this.taxForm.value.type!,
      value: this.taxForm.value.value ?? 0,
      isPercentage: this.taxForm.value.isPercentage ?? false
    }

    this.taxService.addTax(taxRequest).subscribe({
      next: () => {
        this.taxForm.reset();
      },
      error: (err) => {
        console.error('Error adding tax:', err);
      }
    });
  }
}

