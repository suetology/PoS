import { AsyncPipe, NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { CreateServiceRequest, User } from '../../../types';
import { ServiceService } from '../../../services/service.service';

@Component({
  selector: 'app-add-service',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgFor],
  templateUrl: './add-service.component.html',
  styleUrl: './add-service.component.css'
})
export class AddServiceComponent implements OnInit {
  employees: User[] = []
  
  serviceForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    description: new FormControl<string>('', Validators.required),
    price: new FormControl<number>(0, Validators.required),
    duration: new FormControl<number>(0, Validators.required),
    isActive: new FormControl<boolean>(true, Validators.required),
    employeeId: new FormControl<string>('', Validators.required)
  });
  
  constructor(
    private serviceService: ServiceService,
    private userService: UserService) {}
  
  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees() {
    this.userService.getActiveUsers().subscribe(
      (response) => {
        this.employees = response;  
      }
    )
  }

  onSubmit() {
    const request: CreateServiceRequest = {
      name: this.serviceForm.value.name || '',
      description: this.serviceForm.value.description || '',
      price: this.serviceForm.value.price || 0,
      duration: this.serviceForm.value.price || 0,
      employeeId: this.serviceForm.value.employeeId || ''
    }

    this.serviceService.createService(request).subscribe({
      next: () => {
        this.serviceForm.reset();
      },
      error: (err) => {
        console.error('Error creating service:', err);
      }
    });
  }
}
