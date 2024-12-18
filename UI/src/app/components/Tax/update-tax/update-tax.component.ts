import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxService } from '../../../services/tax.service';
import { AsyncPipe, NgIf } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-update-tax',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, AsyncPipe, NgIf],
  templateUrl: './update-tax.component.html',
  styleUrl: './update-tax.component.css'
})
export class UpdateTaxComponent {
  taxForm: FormGroup;
  taxId: string;

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

    this.loadTax();
  }

  loadTax() {
    this.taxService.getTax(this.taxId).subscribe((tax) => {
      this.taxForm.patchValue({
        name: tax.name,
        value: tax.value,
        isPercentage: tax.isPercentage
      });
    });
  }

  onSubmit() {
    if (this.taxForm.invalid) {
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

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }

  goBack() {
    this.router.navigate(['/tax']);
  }
}