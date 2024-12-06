import { Routes } from '@angular/router';
import { ReservationsComponent } from './reservations/reservations.component';
import { ItemsComponent } from './items/items.component';
import { EmployeesComponent } from './employees/employees.component';
import { BusinessComponent } from './business/business.component';
import { DiscountComponent } from './discount/discount/discount.component';
import { ServiceComponent } from './service/service/service.component';
import { ShiftComponent } from './shift/shift/shift.component';
import { TaxComponent } from './Tax/tax/tax.component';
import { OrdersComponent } from './Orders/orders/orders.component';
import { LoginComponent } from './login/login.component';
import { TaxDetailsComponent } from './Tax/tax-details/tax-details.component';
import { DiscountPageComponent } from './discount/discount-page/discount-page.component';
import { TaxPageComponent } from './Tax/tax-page/tax-page.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'business', component: BusinessComponent },
    { path: 'discount', component: DiscountPageComponent },
    { path: 'inventory/item', component: ItemsComponent },
    { path: 'services', component: ServiceComponent },
    { path: 'shifts', component: ShiftComponent },
    { path: 'tax', component: TaxPageComponent,
        children: [
            { path: ':id', component: TaxDetailsComponent },
        ]
     },
    { path: 'employees', component: EmployeesComponent },
    { path: 'reservations', component: ReservationsComponent },
    { path: 'orders', component: OrdersComponent }
];
