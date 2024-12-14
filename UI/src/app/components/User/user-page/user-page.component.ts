import { Component } from '@angular/core';
import { AddUserComponent } from '../add-user/add-user.component';
import { UserComponent } from '../user/user.component';

@Component({
  selector: 'app-user-page',
  standalone: true,
  imports: [AddUserComponent, UserComponent],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.css'
})
export class UserPageComponent {
}
