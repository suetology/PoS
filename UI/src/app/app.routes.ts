import { Routes } from '@angular/router';
import { ReservationsComponent } from './reservations/reservations.component';
import { ItemsComponent } from './items/items.component';
import { BusinessComponent } from './business/business.component';
import { DiscountComponent } from './discount/discount/discount.component';
import { ServiceComponent } from './service/service/service.component';
import { OrdersComponent } from './Orders/orders/orders.component';
import { LoginComponent } from './login/login.component';
import { TaxDetailsComponent } from './Tax/tax-details/tax-details.component';
import { DiscountPageComponent } from './discount/discount-page/discount-page.component';
import { TaxPageComponent } from './Tax/tax-page/tax-page.component';
import { UserPageComponent } from './User/user-page/user-page.component';
import { UserDetailsComponent } from './User/user-details/user-details.component';
import { ServiceChargePageComponent } from './service-charge/service-charge-page/service-charge-page.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'business', component: BusinessComponent },
    { path: 'discount', component: DiscountPageComponent },
    { path: 'inventory/item', component: ItemsComponent },
    { path: 'services', component: ServiceComponent },
    { path: 'tax', component: TaxPageComponent,
        children: [
            { path: ':id', component: TaxDetailsComponent },
        ]
     },
    { path: 'service-charge', component: ServiceChargePageComponent },
    { path: 'user', component: UserPageComponent,
        children: [
            { path: ':id', component: UserDetailsComponent },
        ]
     },
    { path: 'reservations', component: ReservationsComponent },
    { path: 'orders', component: OrdersComponent }
];
