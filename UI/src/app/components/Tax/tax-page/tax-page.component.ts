import { Component } from '@angular/core';
import { TaxComponent } from '../tax/tax.component';
import { AddTaxComponent } from '../add-tax/add-tax.component';

@Component({
  selector: 'app-tax-page',
  standalone: true,
  imports: [TaxComponent, AddTaxComponent],
  templateUrl: './tax-page.component.html',
  styleUrl: './tax-page.component.css'
})
export class TaxPageComponent {

}
