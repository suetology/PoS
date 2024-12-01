import { Routes } from '@angular/router';
import { ReservationsComponent } from './reservations/reservations.component';
import { AppComponent } from './app.component';
import { ItemsComponent } from './items/items.component';
import { EmployeesComponent } from './employees/employees.component';
import { BusinessComponent } from './business/business.component';
import { DiscountComponent } from './discount/discount/discount.component';
import { ServiceComponent } from './service/service/service.component';
import { ShiftComponent } from './shift/shift/shift.component';
import { TaxComponent } from './Tax/tax/tax.component';
import { OrdersComponent } from './Orders/orders/orders.component';

export const routes: Routes = [
    { path: 'business', component: BusinessComponent },
    { path: 'discount', component: DiscountComponent },
    { path: 'inventory/item', component: ItemsComponent },
    { path: 'services', component: ServiceComponent },
    { path: 'shifts', component: ShiftComponent },
    { path: 'tax', component: TaxComponent },
    { path: 'employees', component: EmployeesComponent },
    { path: 'reservations', component: ReservationsComponent },
    { path: 'orders', component: OrdersComponent }
];
