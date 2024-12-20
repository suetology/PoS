import { NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AvailableTime, Service, UpdateReservationRequest } from '../../../types';
import { ServiceService } from '../../../services/service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from '../../../services/order.service';

@Component({
  selector: 'app-update-reservation',
  standalone: true,
  imports: [NgIf, NgFor, FormsModule, ReactiveFormsModule],
  templateUrl: './update-reservation.component.html',
  styleUrl: './update-reservation.component.css'
})
export class UpdateReservationComponent implements OnInit {
  @Output() reservationUpdated = new EventEmitter<UpdateReservationRequest | undefined>();
  updateReservationForm: FormGroup;

  orderId: string;
  availableTimes: AvailableTime[] = [];
  serviceId: string | undefined;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private orderService: OrderService,
    private serviceService: ServiceService,
  ) 
  {
    this.orderId = this.route.snapshot.paramMap.get('id')!;
    this.updateReservationForm = this.fb.group({
      date: [''],
      time: [''],
    });
  }
  ngOnInit(): void {
    var orderResponse = this.orderService.getOrder(this.orderId);
    orderResponse.subscribe((order) => {
      this.serviceId = order.reservation?.service.id;
      // this.updateReservationForm.patchValue({
      //   date: order.reservation?.appointmentTime,
      //   time: order.reservation?.appointmentTime
      // });
    });
  }

  onServiceAndDateSelect() {
    const date = this.updateReservationForm.value.date;

    if (!this.serviceId || !date || this.serviceId == '' || date == '') {
      this.availableTimes = [];

      return;
    }

    this.serviceService.getAvailableTimes(this.serviceId, date).subscribe(
      (response) => {
        this.availableTimes = response ?? [];
      }
    );
  }

  updateReservation() {
    // this.reservationUpdated.emit(this.reservation);
  }

  close() {
    this.reservationUpdated.emit(undefined);
  }
}
