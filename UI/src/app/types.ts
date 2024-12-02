export interface TaxResponse {
    taxes: Tax[];
}  
  
export interface Tax {
    id: string;
    name: string;
    type: TaxType;
    value: number;
    isPercentage: boolean;
    lastUpdated?: Date;
}

export enum TaxType {
    VAT = 1,
    FoodTax,
    AlcoholTax
}

export interface BusinessResponse {
    business: Business[];
}

export interface Business {
    id: string;
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    // employees: Employees;
}

// export interface Employees {
//     id: string;
//     username: string;
//     passwordHash: string;
//     name: string;
//     surname: string;
//     email: string;
//     phoneNumber: string;
//     role: null;
//     dateOfEmployment: Date;
//     lastUpdated: Date;
//     businessId: string;
//     business: Business;
//     shifts: null;
//     services: null;
//     reservations: null;
//     orders: null;

// }
// export interface Item {
//     id: string;
//     name: string;
//     description: string;
//     image: string | null;
//     price: number;
//     stock: number;
//     itemGroupId?: string;
//     itemGroup?: ItemGroup;
//     itemTaxes?: ItemTax[];
//     itemDiscounts?: ItemDiscount[];
// }

// export interface ItemGroup {
//     id: string;
//     name: string;
//     description: string;
//     items: Item;
//     groupDiscounts: GroupDiscount;
// }

// export interface GroupDiscount {
//     id: string;
//     discountId: string;
//     discount: Discount;
//     itemGroupId: string;
//     itemGroup: ItemGroup;
// }

// export interface ItemTax {
//     id: string;
//     taxId: string;
//     tax: Tax;
//     itemId: string;
//     item: Item;
// }

// export interface ItemDiscount {
//     id: string;
//     discountId: string;
//     discount: Discount;
//     itemId: string;
//     item: Item;
// }

// export interface Discount {
//     id: string;
//     name: string;
//     value: number;
//     isPercentage: boolean;
//     amountAvailable: number;
//     validFrom: Date;
//     validTo: Date;
//     itemDiscounts: ItemDiscount;
//     groupDiscounts: GroupDiscount;
// }