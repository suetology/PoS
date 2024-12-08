import { AsyncPipe, NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Service } from '../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { ServiceService } from '../../services/service.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-service-details',
  standalone: true,
  imports: [AsyncPipe, NgIf],
  templateUrl: './service-details.component.html',
  styleUrl: './service-details.component.css'
})
export class ServiceDetailsComponent implements OnInit {
  service: Service | undefined;
  employeeUsername: string | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceService: ServiceService,
    private userService: UserService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.serviceService.getService(id).subscribe(
        (service) => {
          this.service = service;

          this.userService.getUser(this.service?.employeeId).subscribe(
            (user => {
              this.employeeUsername = user.username
            })
          );
        },
        (error) => {
          console.error('Error fetching service details:', error);
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
}
