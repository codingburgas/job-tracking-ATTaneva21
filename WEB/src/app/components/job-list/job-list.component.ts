import { Component } from '@angular/core';

@Component({
  selector: 'app-job-list',
  standalone: true,
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent {
  jobs = [
    { title: 'Frontend Developer', description: 'Build responsive UIs', location: 'Remote' },
    { title: 'Backend Developer', description: 'Work with .NET APIs', location: 'Sofia' }
  ];
}
