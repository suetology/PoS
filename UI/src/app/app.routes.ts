import { Routes } from '@angular/router';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { BusinessComponent } from './components/business/business.component';
import { LoginComponent } from './components/login/login.component';
import { TaxDetailsComponent } from './components/Tax/tax-details/tax-details.component';
import { DiscountPageComponent } from './components/discount/discount-page/discount-page.component';
import { TaxPageComponent } from './components/Tax/tax-page/tax-page.component';
import { UserPageComponent } from './components/User/user-page/user-page.component';
import { UserDetailsComponent } from './components/User/user-details/user-details.component';
import { ServiceChargePageComponent } from './components/service-charge/service-charge-page/service-charge-page.component';
import { ServicePageComponent } from './components/service/service-page/service-page.component';
import { ServiceDetailsComponent } from './components/service/service-details/service-details.component';
import { ItemGroupPageComponent } from './components/item-group/item-group-page/item-group-page.component';
import { ItemGroupDetailsComponent } from './components/item-group/item-group-details/item-group-details.component';
import { ItemPageComponent } from './components/Item/item-page/item-page.component';
import { ItemDetailsComponent } from './components/Item/item-details/item-details.component';
import { DiscountDetailsComponent } from './components/discount/discount-details/discount-details.component';
import { OrderPageComponent } from './components/Order/order-page/order-page.component';
import { OrderDetailsComponent } from './components/Order/order-details/order-details.component';
import { AddItemsToOrderComponent } from './components/Order/add-items-to-order/add-items-to-order.component';
import { AddReservationToOrderComponent } from './components/Order/add-reservation-to-order/add-reservation-to-order.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'business', component: BusinessComponent },
    { path: 'discount', component: DiscountPageComponent,
        children: [
            { path: ':id', component: DiscountDetailsComponent },
        ]
    },
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
