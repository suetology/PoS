import { Routes } from '@angular/router';
import { ReservationsComponent } from './components/reservations/reservations.component';
import { BusinessComponent } from './components/business/business/business.component';
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
import { AuthGuard } from './guards/auth.guard';
import { UpdateBusinessComponent } from './components/business/update-business/update-business.component';
// import { UpdateUserComponent } from './components/User/update-user/update-user.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full'},
    { path: 'login', component: LoginComponent },
    { path: 'business', component: BusinessComponent, canActivate: [AuthGuard], 
        children: [
            { path: ':id/edit', component: UpdateBusinessComponent }
        ]
    },
    { path: 'discount', component: DiscountPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: DiscountDetailsComponent, canActivate: [AuthGuard] },
        ]
    },
    { path: 'item-group', component: ItemGroupPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ItemGroupDetailsComponent, canActivate: [AuthGuard] },
        ]
     },
    { path: 'item', component: ItemPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ItemDetailsComponent, canActivate: [AuthGuard], }
        ]
     },
    { path: 'services', component: ServicePageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ServiceDetailsComponent, canActivate: [AuthGuard] },
        ]
     },
    { path: 'tax', component: TaxPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: TaxDetailsComponent, canActivate: [AuthGuard] },
        ]
     },
    { path: 'service-charge', component: ServiceChargePageComponent, canActivate: [AuthGuard] },
    { path: 'user', component: UserPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: UserDetailsComponent, canActivate: [AuthGuard] },
            // { path: 'edit', component: UpdateUserComponent, canActivate: [AuthGuard] } 
        ]
     },
    { path: 'reservations', component: ReservationsComponent, canActivate: [AuthGuard] },
    { path: 'order', component: OrderPageComponent, canActivate: [AuthGuard],
        children: [
            { path: 'add-items', component: AddItemsToOrderComponent, canActivate: [AuthGuard]},
            { path: 'add-reservation', component: AddReservationToOrderComponent, canActivate: [AuthGuard] },
            { path: ':id', component: OrderDetailsComponent, canActivate: [AuthGuard] },
        ]
     }
];
