import { Component, OnInit } from '@angular/core';
import { CreateShiftRequest, User } from '../../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShiftService } from '../../../services/shift.service';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [AsyncPipe, NgIf, NgFor, FormsModule, ReactiveFormsModule],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {
  user: User | undefined;
  
  shiftForm = new FormGroup({
    date: new FormControl<string>('', [Validators.required]),
    startTime: new FormControl<string>('', [Validators.required]),
    endTime: new FormControl<string>('', [Validators.required])
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService,
    private shiftService: ShiftService
  ) { }

  addShift() {
    if (!this.shiftForm.valid) {
      return;
    }

    const request: CreateShiftRequest = {
      date: this.shiftForm.value.date || '',
      startTime: this.shiftForm.value.startTime || '',
      endTime: this.shiftForm.value.endTime || '',
      employeeId: this.user?.id || ''
    }

    this.shiftService.createShift(request).subscribe({
      next: () => {
        this.shiftForm.reset();

        if (!this.user) {
          return;
        }

        this.userService.getUser(this.user.id).subscribe(
          (user) => {
            this.user = user;
          },
          (error) => {
            console.error('Error fetching user details:', error);
            this.close();
          }
        );
      },
      error: (err) => {
        console.log("Error creating shift: ", err);
      }
    })
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.userService.getUser(id).subscribe(
        (user) => {
          this.user = user;
        },
        (error) => {
          console.error('Error fetching user details:', error);
          this.close();
        }
      );
    } else {
      this.close();
    }
  }

  onDelete(shiftId:string){
    if (!this.user) return;

    this.shiftService.deleteShift(shiftId).subscribe({
      next: (value) => {
          alert('Shift deleted');

          this.userService.getUser(this.user!.id).subscribe({
            next: (updatedUser) => {
              this.user = updatedUser;
            }
        });
      }
    });
  }

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
