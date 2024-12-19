import { Component } from '@angular/core';
import { Tax } from '../../../types';
import { filter, Observable, Subscription } from 'rxjs';
import { TaxService } from '../../../services/tax.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-tax',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './tax.component.html',
  styleUrl: './tax.component.css'
})
export class TaxComponent {
  
  taxes$: Observable<Tax[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private taxService : TaxService, 
    private router: Router,
    private route: ActivatedRoute){

    this.taxes$ = this.taxService.getTaxes();

    this.updateSub = this.taxService.getTaxesUpdated().subscribe(() => {
      this.taxes$ = this.taxService.getTaxes();
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

  trackById(index: number, tax: Tax): string {
    return tax.id;
  }

  goToTaxDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editTax(taxId: string) {
    this.router.navigate([`/tax/${taxId}/edit`]);
  }

  retireTax(taxId: string) {
    this.taxService.retireTax(taxId).subscribe({
      next: () => {
        this.router.navigate(['/tax']);
      },
      error: (error) => {
        console.error('Error retiring tax:', error);
      },
    });
}
}
