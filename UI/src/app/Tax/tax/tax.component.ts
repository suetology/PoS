import { Component } from '@angular/core';
import { Tax } from '../../types';
import { Observable } from 'rxjs';
import { TaxService } from '../../services/tax.service';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-tax',
  standalone: true,
  imports: [CommonModule, AsyncPipe],
  templateUrl: './tax.component.html',
  styleUrl: './tax.component.css'
})
export class TaxComponent {
  
  taxes$: Observable<Tax[]>;

  constructor(private taxService : TaxService){
    this.taxes$ = this.taxService.getTaxes();
  }
}
