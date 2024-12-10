import { Routes } from '@angular/router';
import { ReservationsComponent } from './reservations/reservations.component';
import { BusinessComponent } from './business/business.component';
import { LoginComponent } from './login/login.component';
import { TaxDetailsComponent } from './Tax/tax-details/tax-details.component';
import { DiscountPageComponent } from './discount/discount-page/discount-page.component';
import { TaxPageComponent } from './Tax/tax-page/tax-page.component';
import { UserPageComponent } from './User/user-page/user-page.component';
import { UserDetailsComponent } from './User/user-details/user-details.component';
import { ServiceChargePageComponent } from './service-charge/service-charge-page/service-charge-page.component';
import { ServicePageComponent } from './service/service-page/service-page.component';
import { ServiceDetailsComponent } from './service/service-details/service-details.component';
import { ItemGroupPageComponent } from './item-group/item-group-page/item-group-page.component';
import { ItemGroupDetailsComponent } from './item-group/item-group-details/item-group-details.component';
import { ItemPageComponent } from './Item/item-page/item-page.component';
import { ItemDetailsComponent } from './Item/item-details/item-details.component';
import { OrderPageComponent } from './Order/order-page/order-page.component';
import { OrderDetailsComponent } from './Order/order-details/order-details.component';
import { AddItemsToOrderComponent } from './Order/add-items-to-order/add-items-to-order.component';
import { AddReservationToOrderComponent } from './Order/add-reservation-to-order/add-reservation-to-order.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'business', component: BusinessComponent },
    { path: 'discount', component: DiscountPageComponent },
    { path: 'item-group', component: ItemGroupPageComponent,
        children: [
            { path: ':id', component: ItemGroupDetailsComponent },
        ]
     },
    { path: 'item', component: ItemPageComponent,
        children: [
            { path: ':id', component: ItemDetailsComponent }
        ]
     },
    { path: 'services', component: ServicePageComponent,
        children: [
            { path: ':id', component: ServiceDetailsComponent },
        ]
     },
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
    { path: 'order', component: OrderPageComponent,
        children: [
            { path: 'add-items', component: AddItemsToOrderComponent },
            { path: 'add-reservation', component: AddReservationToOrderComponent },
            { path: ':id', component: OrderDetailsComponent },
        ]
     }
];
