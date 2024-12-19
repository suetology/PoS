import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AvailableTime, CreateReservationRequest, Service } from '../../../types';
import { ServiceService } from '../../../services/service.service';

@Component({
  selector: 'app-add-reservation-to-order',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule],
  templateUrl: './add-reservation-to-order.component.html',
  styleUrl: './add-reservation-to-order.component.css'
})
export class AddReservationToOrderComponent implements OnInit {
  @Output() reservationAdded = new EventEmitter<CreateReservationRequest | undefined>();

  reservation: CreateReservationRequest | undefined = undefined;
  services: Service[] = [];
  availableTimes: AvailableTime[] = [];

  reservationForm = new FormGroup({
    serviceId: new FormControl<string>('', Validators.required),
    date: new FormControl<string>('', Validators.required),
    time: new FormControl<string>('', Validators.required)
  });
  
  constructor(private serviceService: ServiceService) { }

  ngOnInit() {
    this.serviceService.getServices().subscribe(
      (response) => {
        this.services = response;
      }
    );
  }

  onServiceAndDateSelect() {
    const serviceId = this.reservationForm.value.serviceId;
    const date = this.reservationForm.value.date;

    if (!serviceId || !date || serviceId == '' || date == '') {
      this.availableTimes = [];

      return;
    }

    this.serviceService.getAvailableTimes(serviceId, date).subscribe(
      (response) => {
        this.availableTimes = response ?? [];
      }
    );
  }

  addReservation() {
    this.reservation = {
      serviceId: this.reservationForm.value.serviceId || '',
      appointmentTime: (this.reservationForm.value.date || '') + 'T' + (this.reservationForm.value.time || '')
    }

    this.reservationAdded.emit(this.reservation);
  }

  close() {
    this.reservationAdded.emit(undefined);
  }
}
