import { Component, OnInit } from '@angular/core';
import { Tax } from '../../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxService } from '../../../services/tax.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-tax-details',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, NgIf],
  templateUrl: './tax-details.component.html',
  styleUrl: './tax-details.component.css'
})
export class TaxDetailsComponent implements OnInit{
  taxForm: FormGroup;
  taxId: string;
  
  tax: Tax | undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private taxService: TaxService
  ) {
    this.taxId = this.route.snapshot.paramMap.get('id')!;
    this.taxForm = this.fb.group({
      name: [''],
      value: [''],
      isPercentage: ['']
    });

    this.loadItem();
  }

  loadItem() {
    this.taxService.getTax(this.taxId).subscribe((tax) => {
      this.taxForm.patchValue({
        name: tax.name,
        value: tax.value,
        isPercentage: tax.isPercentage
      });
    });
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.taxService.getTax(id).subscribe(
        (tax) => {
          this.tax = tax;
        },
        (error) => {
          console.error('Error fetching tax details:', error);
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

  onSubmit() {
    if (!this.tax || this.taxForm.invalid) {
      return;
    }

    const request = this.taxForm.value;
    this.taxService.updateTax(this.taxId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/tax']);
        },
        error: (error) => {
          console.error('Error updating tax:', error);
        },
      });
  }
}
