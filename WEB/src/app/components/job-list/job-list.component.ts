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
  jobs = [ {
    title: 'Frontend Developer',
    description: 'Create responsive web interfaces using Angular and TypeScript.',
    location: 'Remote'
  },
    {
      title: 'Backend Developer',
      description: 'Build RESTful APIs and manage databases using .NET Core.',
      location: 'Sofia, Bulgaria'
    },
    {
      title: 'UI/UX Designer',
      description: 'Design intuitive user experiences and interactive prototypes.',
      location: 'Plovdiv, Bulgaria'
    },
    {
      title: 'Data Analyst',
      description: 'Analyze datasets and create dashboards for internal reporting.',
      location: 'Varna, Bulgaria'
    },
    {
      title: 'DevOps Engineer',
      description: 'Automate deployment and manage CI/CD pipelines.',
      location: 'Remote'
    },
    {
      title: 'QA Tester',
      description: 'Perform manual and automated testing to ensure product quality.',
      location: 'Burgas, Bulgaria'
    },
    {
      title: 'Mobile App Developer',
      description: 'Develop Android/iOS apps using Flutter or React Native.',
      location: 'Remote'
    },
    {
      title: 'Project Manager',
      description: 'Lead agile teams and ensure timely delivery of milestones.',
      location: 'Sofia, Bulgaria'
    } ];

  currentPage = 1;
  pageSize = 3;

  get paginatedJobs() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.jobs.slice(start, start + this.pageSize);
  }

  get totalPages() {
    return Math.ceil(this.jobs.length / this.pageSize);
  }

  changePage(direction: 'prev' | 'next') {
    if (direction === 'prev' && this.currentPage > 1) {
      this.currentPage--;
    } else if (direction === 'next' && this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}
