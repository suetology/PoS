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
    isPercentage: boolean;
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

export interface UpdateBusinessRequest  {
    name?: string;
    address?: string;
    phoneNumber?: string;
    email?: string;
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

export interface UpdateUserRequest {
    username?: string,
    passwordHash?: string,
    name?: string,
    surname?: string,
    email?: string,
    phoneNumber?: string,
    role?: Role,
    status?: EmployeeStatus
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

export interface CreateServiceRequest {
    name: string,
    description: string,
    price: number,
    duration: number,
    isActive: boolean,
    employeeId: string
}

export interface UpdateServiceRequest {
    name?: string,
    description?: string,
    price?: number,
    duration?: number,
    isActive?: boolean,
    employeeId?: string
}

export interface GetAllServicesRequest {
    
}

export interface GetAllServicesResponse {
    services: Service[]
}

export interface GetServiceResponse {
    service: Service
}

export interface Service {
    id: string,
    name: string,
    description: string,
    price: number,
    duration: number,
    isActive: boolean,
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

export interface GroupDiscount {
    id: string;
    discountId: string;
    discount: Discount;
    itemGroupId: string;
    itemGroup: ItemGroup;
}

export interface DiscountResponse {
    discounts: Discount[];
}

export interface Discount {
    id: string;
    name: string;
    value: number;
    isPercentage: boolean;
    amountAvailable: number;
    validFrom: Date;
    validTo: Date;
    items: Item[];
    itemGroups: ItemGroup[];
}

export interface DiscountRequest {
    name: string;
    value: number;
    isPercentage: boolean;
    amountAvailable?: number;
    validFrom?: string;
    validTo?: string;
    applicableOrder?: string,
    applicableItems?: string[];
    applicableGroups?: string[];
}

export interface ServiceCharge {
    id: string;
    name: string;
    description: string;
    value: number;
    isPercentage: boolean;
}

export interface ServiceChargeResponse {
    serviceCharges: ServiceCharge[];
}

export interface ServiceChargeRequest {
    name: string;
    description: string;
    value: number;
    isPercentage: boolean;
}

export interface ItemGroup {
    id: string,
    name: string,
    description: string
    discounts: Discount[]
}

export interface CreateItemGroupRequest {
    name: string,
    description: string
}

export interface GetItemGroupResponse {
    itemGroup: ItemGroup
}

export interface GetAllItemGroupsRequest {

}

export interface GetAllItemGroupsResponse {
    itemGroups: ItemGroup[]    
}

export interface UpdateItemGroupRequest {
    name?: string,
    description?: string
}

export interface Item {
    id: string,
    name: string,
    description: string,
    price: number, 
    stock: number,
    image: string,
    itemGroupId?: string,
    itemGroup?: ItemGroup,
    discounts: Discount[],
    taxIds: string[],
    taxes: Tax[]
}

export interface CreateItemRequest {
    name: string,
    description: string,
    price: number,
    stock: number,
    image: string,
    itemGroupId?: string,
    taxIds: string[]
}

export interface UpdateItemRequest {
    name?: string,
    description?: string,
    price?: number, 
    stock?: number,
    image?: string,
    itemGroupId?: string,
    taxIds?: string[]
}

export interface GetItemResponse {
    item: Item
}

export interface GetAllItemsRequest {

}

export interface GetAllItemsResponse {
    items: Item[]
}

export interface ItemVariation {
    id: string,
    name: string,
    description: string,
    addedPrice: number,
    stock: number,
    itemId: string
}

export interface CreateItemVariationRequest {
    name: string,
    description: string,
    addedPrice: number,
    stock: number
}

export interface GetItemVariationResponse {
    itemVariation: ItemVariation
}

export interface GetAllItemVariationsRequest {

}

export interface GetAllItemVariationsResponse {
    itemVariations: ItemVariation[]
}

export enum OrderStatus {
    Open = 1,
    Closed,
    Paid,
    Canceled,
    Refunded
}

export interface OrderItem {
    id: string,
    quantity: number,
    item: Item,
    itemVariations: ItemVariation[]
}

export interface Order {
    id: string,
    status: OrderStatus,
    created: string,
    closed?: string,
    finalAmount: number,
    paidAmount: number,
    tipAmount: number,
    employee: User,
    customer: Customer,
    serviceCharge?: ServiceCharge,
    orderItems: OrderItem[],
    reservation?: Reservation,
    discount?: Discount

    // add payments
    // add refund
}

export interface AddTipRequest {
    tipAmount: number
}

export interface GetOrderRespose {
    order: Order
}

export interface GetAllOrdersRequest {

}

export interface GetAllOrdersResponse {
    orders: Order[]
}

export interface AvailableTime {
    start: string,
    end: string
}

export interface GetAvailableTimesResponse {
    availableTimes: AvailableTime[]
} 

export enum AppointmentStatus
{
    Booked = 1,
    Cancelled,
    Completed,
    Refunded
}

export interface Reservation {
    id: string,
    status: AppointmentStatus,
    reservationTime: string,
    appointmentTime: string,
    service: Service
}

export interface CreateReservationRequest {
    appointmentTime: string,
    serviceId: string
}

export interface CreateOrderItemRequest {
    itemId: string,
    itemVariationsIds: string[],
    quantity: number
}

export interface Customer {
    id: string
    name: string,
    email: string,
    phoneNumber: string
}

export interface CreateCustomerRequest {
    name: string,
    email: string,
    phoneNumber: string
}

export interface GetCustomerResponse {
    customer: Customer
}

export interface GetAllCustomersResponse {
    customers: Customer[]
}

export interface CreateOrderRequest {
    customerId?: string,
    customer?: CreateCustomerRequest,
    serviceChargeId?: string,
    reservation?: CreateReservationRequest
    orderItems: CreateOrderItemRequest[]
}