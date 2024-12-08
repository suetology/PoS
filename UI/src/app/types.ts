export interface LoginRequest {
    username: string,
    password: string
}

export interface LoginResponse { 
    accessToken: string, 
    refreshToken: string
}

export interface LogoutRequest {
    refreshToken: string
}

export interface RefreshAccessTokenRequest {
    refreshToken: string
}

export interface RefreshAccessTokenResponse {
    refreshToken: string,
    accessToken: string
}

export interface TaxResponse {
    taxes: Tax[];
}  

export interface TaxRequest {
    name: string;
    type: TaxType;
    value: number;
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
    FoodTax = 2,
    AlcoholTax = 3
}

export interface BusinessResponse {
    businesses: Business[];
}

export interface Business {
    id: string;
    name: string;
    address: string;
    phoneNumber: string;
    email: string;
    // employees: Employees;
}

export interface CreateUserRequest {
    username: string,
    passwordHash: string,
    name: string,
    surname: string,
    email: string,
    phoneNumber: string,
    role: Role,
    status: EmployeeStatus
}

export interface GetAllUsersRequest {

}

export interface GetAllUsersResponse {
    users: User[]
}

export interface User {
    id: string, 
    username: string,
    passwordHash: string,
    name: string,
    surname: string,
    email: string,
    phoneNumber: string,
    role: Role,
    status: EmployeeStatus,
    dateOfEmployment: string,
    shifts: Shift[]
}

export interface Shift {
    id: string, 
    date: string,
    startTime: string,
    endTime: string
}

export interface CreateShiftRequest {
    date: string,
    startTime: string,
    endTime: string,
    employeeId: string
}

export enum Role {
    SuperAdmin = 'SuperAdmin',
    BusinessOwner = 'BusinessOwner',
    Employee = 'Employee'
}

export enum EmployeeStatus {
    Active = 1,
    Left,
    Fired,
    UnpaidLeave,
    PaidLeave,
    SickLeave,
    ChildcareLeave,
    NotEmployee
}

export interface Item {
    id: string;
    name: string;
    description: string;
    image: string | null;
    price: number;
    stock: number;
    itemGroupId?: string;
    itemGroup?: ItemGroup;
    itemTaxes?: ItemTax[];
    itemDiscounts?: ItemDiscount[];
}

export interface ItemGroup {
    id: string;
    name: string;
    description: string;
    items: Item;
    groupDiscounts: GroupDiscount;
}

export interface GroupDiscount {
    id: string;
    discountId: string;
    discount: Discount;
    itemGroupId: string;
    itemGroup: ItemGroup;
}

export interface ItemTax {
    id: string;
    taxId: string;
    tax: Tax;
    itemId: string;
    item: Item;
}

export interface ItemDiscount {
    id: string;
    discountId: string;
    discount: Discount;
    itemId: string;
    item: Item;
}

export interface DiscountResponse{
    discount: Discount[];
}

export interface Discount {
    discountId: string;
    discountName: string;
    value: number;
    isPercentage: boolean;
    amountAvailable: number;
    validFrom: Date;
    validTo: Date;
    itemGroups?: GroupDiscount;
}

export interface DiscountRequest {
    discountName: string;
    value: number;
    isPercentage: boolean;
    amountAvailable: number;
    validFrom: Date;
    validTo: Date;
}