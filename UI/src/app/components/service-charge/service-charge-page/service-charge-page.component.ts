import { Component } from '@angular/core';
import { ServiceChargeComponent } from '../service-charge/service-charge.component';
import { AddServiceChargeComponent } from '../add-service-charge/add-service-charge.component';

@Component({
  selector: 'app-service-charge-page',
  standalone: true,
  imports: [ServiceChargeComponent, AddServiceChargeComponent],
  templateUrl: './service-charge-page.component.html',
  styleUrl: './service-charge-page.component.css'
})
export class ServiceChargePageComponent {

}
