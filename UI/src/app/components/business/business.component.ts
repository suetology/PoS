import { Component } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { Business } from '../../types';
import { BusinessService } from '../../services/business.service';

@Component({
  selector: 'app-business',
  standalone: true,
  imports: [CommonModule, AsyncPipe],
  templateUrl: './business.component.html',
  styleUrl: './business.component.css'
})
export class BusinessComponent {

  business$: Observable<Business[]>;

  constructor(private businessService : BusinessService){
    this.business$ = this.businessService.getBusiness();
  }
}

