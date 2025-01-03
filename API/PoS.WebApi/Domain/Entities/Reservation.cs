﻿using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class Reservation : Entity
{
    public Guid BusinessId { get; set; }
    
    public DateTime ReservationTime { get; set; }
    
    public DateTime AppointmentTime { get; set; }
    
    public AppointmentStatus Status { get; set; }
    
    public bool NotificationSent { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public Service Service { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}