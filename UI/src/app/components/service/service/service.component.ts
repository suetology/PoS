import { AsyncPipe, CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { Service } from '../../../types';
import { filter, Observable, Subscription } from 'rxjs';
import { ServiceService } from '../../../services/service.service';

@Component({
  selector: 'app-service',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './service.component.html',
  styleUrl: './service.component.css'
})
export class ServiceComponent {
  services$: Observable<Service[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private serviceService: ServiceService, 
    private router: Router,
    private route: ActivatedRoute) {

    this.services$ = this.serviceService.getServices();

    this.updateSub = this.serviceService.getServicesUpdated().subscribe(() => {
      this.services$ = this.serviceService.getServices();
    });

    this.routeSub = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, service: Service): string {
    return service.id;
  }

  goToServiceDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editService(serviceId: string) {
    this.router.navigate([`/services/${serviceId}/edit`]);
  }
}
