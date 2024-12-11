import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { BusinessService } from '../../../services/business.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-update-business',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './update-business.component.html',
  styleUrl: './update-business.component.css'
})
export class UpdateBusinessComponent {
  businessForm: FormGroup;
  businessId: string;

  constructor(
    private fb: FormBuilder,
    private businessService: BusinessService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.businessId = this.route.snapshot.paramMap.get('id')!;
    this.businessForm = this.fb.group({
      name: ['', Validators.required],
      address: [''],
      phoneNumber: [''],
      email: ['', [Validators.email]],
    });

    this.loadBusiness();
  }

  loadBusiness() {
    this.businessService.getBusinessById(this.businessId).subscribe((business) => {
      this.businessForm.patchValue(business);
    });
  }

  onSubmit() {
    if (this.businessForm.invalid) {
      return;
    }

    this.businessService.updateBusiness(this.businessId, this.businessForm.value)
      .subscribe({
        next: () => {
          this.router.navigate(['/business']);
        },
        error: (error) => {
          console.error('Error updating business:', error);
        },
      });
  }

  goBack() {
    this.router.navigate(['/business']);
  }
  
}
