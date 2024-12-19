import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { User } from '../../../types';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { ServiceService } from '../../../services/service.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-update-service',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgFor],
  templateUrl: './update-service.component.html',
  styleUrl: './update-service.component.css'
})
export class UpdateServiceComponent {
  serviceForm: FormGroup;
  serviceId: string;

  employees: User[] = [];

  private sub: Subscription | undefined = undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private userService: UserService,
    private serviceService: ServiceService
  ) {
    this.serviceId = this.route.snapshot.paramMap.get('id')!;
    this.serviceForm = this.fb.group({
      name: [''],
      description: [''],
      price: [''],
      duration: [''],
      employeeId: [[]]
    });

    this.loadItem();
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees() {
    this.userService.getUsers().subscribe(
      (response) => {
        this.employees = response;  
      }
    )
  }

  loadItem() {
    this.serviceService.getService(this.serviceId).subscribe((service) => {
      this.serviceForm.patchValue({
        name: service.name,
        description: service.description,
        price: service.price,
        duration: service.duration,
        employeeId: service.employeeId || []
      });
    });
  }

  onSubmit() {
    if (this.serviceForm.invalid) {
      return;
    }

    const request = this.serviceForm.value;

    this.serviceService.updateService(this.serviceId, request)
      .subscribe({
        next: () => {
          this.router.navigate(['/services']);
        },
        error: (error) => {
          console.error('Error updating service:', error);
        },
      });
  }

  goBack() {
    this.router.navigate(['/services']);
  }

  ngOnDestroy() {
    if (this.sub) {
      this.sub.unsubscribe();
    }
  }
}