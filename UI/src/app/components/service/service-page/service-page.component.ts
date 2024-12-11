import { Component } from '@angular/core';
import { ServiceComponent } from '../service/service.component';
import { AddServiceComponent } from '../add-service/add-service.component';

@Component({
  selector: 'app-service-page',
  standalone: true,
  imports: [ServiceComponent, AddServiceComponent],
  templateUrl: './service-page.component.html',
  styleUrl: './service-page.component.css'
})
export class ServicePageComponent {

}
