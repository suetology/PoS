import { Component, OnInit } from '@angular/core';
import { User } from '../../types';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { AsyncPipe, NgIf } from '@angular/common';

@Component({
  selector: 'app-user-details',
  standalone: true,
  imports: [AsyncPipe, NgIf],
  templateUrl: './user-details.component.html',
  styleUrl: './user-details.component.css'
})
export class UserDetailsComponent implements OnInit {

  user: User | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    console.log(id);
    if (id) {
      this.userService.getUser(id).subscribe(
        (user) => {
          console.log(user);
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

  close() {
    this.router.navigate(['../'], { relativeTo: this.route });
  }
}
