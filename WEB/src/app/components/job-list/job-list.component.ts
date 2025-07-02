import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent {
  jobs = [
    {
      title: 'Frontend Developer',
      description: 'Build modern and responsive UIs with Angular.',
      location: 'Remote'
    },
    {
      title: 'Backend Developer',
      description: 'Develop RESTful APIs using .NET Core.',
      location: 'Sofia, Bulgaria'
    }
  ];
}
