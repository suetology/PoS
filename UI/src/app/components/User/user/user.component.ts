import { Component } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { filter, Observable, Subscription } from 'rxjs';
import { User } from '../../../types';
import { UserService } from '../../../services/user.service';
import { AsyncPipe, CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterOutlet],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  
  users$: Observable<User[]>;
  isModalOpen = false;
  private routeSub: Subscription;
  private updateSub: Subscription;

  constructor(private userService : UserService, 
    private router: Router,
    private route: ActivatedRoute){

    this.users$ = this.userService.getUsers();

    this.updateSub = this.userService.getUsersUpdated().subscribe(() => {
      this.users$ = this.userService.getUsers();
    });

    this.routeSub = this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.isModalOpen = !!this.route.firstChild;
      });
  }

  ngOnDestroy() {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
    if (this.updateSub) {
      this.updateSub.unsubscribe();
    }
  }

  trackById(index: number, user: User): string {
    return user.id;
  }

  goToUserDetails(id: string) {
    this.router.navigate([id], { relativeTo: this.route });
  }

  editUser(userId: string) {
    alert("Will be implemented later");
    //this.router.navigate([`/user/${userId}/edit`]);
  }
}
