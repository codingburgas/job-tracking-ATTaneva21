import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent {
  user = {
    firstName: 'Jane',
    lastName: 'Doe',
    email: 'jane@example.com',
    education: 'MSc Computer Science',
    experience: '3 years in software development'
  };
}
