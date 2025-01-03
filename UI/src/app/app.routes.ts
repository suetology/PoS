import { Routes } from '@angular/router';
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
import { UpdateUserComponent } from './components/User/update-user/update-user.component';
import { UpdateItemGroupComponent } from './components/item-group/update-item-group/update-item-group.component';
import { UpdateItemComponent } from './components/Item/update-item/update-item.component';
import { UpdateServiceComponent } from './components/service/update-service/update-service.component';
import { BusinessesComponent } from './components/businesses/businesses/businesses.component';
import { UpdateTaxComponent } from './components/Tax/update-tax/update-tax.component';
import { UpdateServiceChargeComponent } from './components/service-charge/update-service-charge/update-service-charge.component';
import { ServiceChargeDetailsComponent } from './components/service-charge/service-charge-details/service-charge-details.component';
import { CustomerPageComponent } from './components/customer/customer-page/customer-page.component';
import { CustomerDetailsComponent } from './components/customer/customer-details/customer-details.component';
import { UpdateCustomerComponent } from './components/customer/update-customer/update-customer.component';

export const routes: Routes = [
    { path: '', redirectTo: '/login', pathMatch: 'full'},
    { path: 'login', component: LoginComponent },
    { path: 'businesses', component: BusinessesComponent, canActivate: [AuthGuard] },
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
            { path: ':id/edit', component: UpdateItemGroupComponent, canActivate: [AuthGuard] } 
        ]
     },
    { path: 'item', component: ItemPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ItemDetailsComponent, canActivate: [AuthGuard], },
            { path: ':id/edit', component: UpdateItemComponent, canActivate: [AuthGuard] } 
        ]
     },
    { path: 'services', component: ServicePageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ServiceDetailsComponent, canActivate: [AuthGuard] },
            { path: ':id/edit', component: UpdateServiceComponent, canActivate: [AuthGuard] } 
        ]
     },
    { path: 'tax', component: TaxPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: TaxDetailsComponent, canActivate: [AuthGuard] },
            { path: ':id/edit', component: UpdateTaxComponent, canActivate: [AuthGuard] },
        ]
     },
    { path: 'service-charge', component: ServiceChargePageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: ServiceChargeDetailsComponent, canActivate: [AuthGuard] },
            { path: ':id/edit', component: UpdateServiceChargeComponent, canActivate: [AuthGuard] } 
        ]
    },
    { path: 'user', component: UserPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: UserDetailsComponent, canActivate: [AuthGuard] },
            { path: ':id/edit', component: UpdateUserComponent, canActivate: [AuthGuard] } 
        ]
     },
     { path: 'customer', component: CustomerPageComponent, canActivate: [AuthGuard],
        children: [
            { path: ':id', component: CustomerDetailsComponent, canActivate: [AuthGuard] },
            { path: ':id/edit', component: UpdateCustomerComponent, canActivate: [AuthGuard] } 
        ]
     },
    { path: 'order', component: OrderPageComponent, canActivate: [AuthGuard],
        children: [
            { path: 'add-items', component: AddItemsToOrderComponent, canActivate: [AuthGuard]},
            { path: 'add-reservation', component: AddReservationToOrderComponent, canActivate: [AuthGuard] },
            { path: ':id', component: OrderDetailsComponent, canActivate: [AuthGuard] },
        ]
     }
];
