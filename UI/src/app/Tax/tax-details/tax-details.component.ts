import { Component, OnInit } from '@angular/core';
import { Tax } from '../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxService } from '../../services/tax.service';
import { AsyncPipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-tax-details',
  standalone: true,
  imports: [AsyncPipe, NgIf],
  templateUrl: './tax-details.component.html',
  styleUrl: './tax-details.component.css'
})
export class TaxDetailsComponent implements OnInit{

  tax: Tax | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taxService: TaxService
  ) {}

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
}
