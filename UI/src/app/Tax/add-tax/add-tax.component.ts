import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaxRequest } from '../../types';
import { TaxService } from '../../services/tax.service';

@Component({
  selector: 'app-add-tax',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-tax.component.html',
  styleUrl: './add-tax.component.css'
})
export class AddTaxComponent {

  constructor(private taxService: TaxService) {}
  
  onTaxCreate(tax: {name: string, type: string, value: number, isPercentage: boolean}){
    console.log(tax);
    const taxRequest: TaxRequest = {
      name: tax.name,
      type: +tax.type,
      value: tax.value,
      isPercentage: tax.isPercentage
    };

    this.taxService.addTax(taxRequest).subscribe({
      next: (response) => {
        console.log('Tax created successfully:', response);
      },
      error: (err) => {
        console.error('Error creating tax:', err);
      }
    });
  }
}

