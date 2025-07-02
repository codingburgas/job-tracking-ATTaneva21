import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-job-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './job-list.component.html',
  styleUrls: ['./job-list.component.scss']
})
export class JobListComponent {
  searchTerm: string = '';
  currentPage: number = 1;
  pageSize: number = 3;

  jobs = [
    { title: 'Frontend Developer', description: 'Create responsive UIs using Angular.', location: 'Remote' },
    { title: 'Backend Developer', description: 'Build APIs with .NET Core.', location: 'Sofia' },
    { title: 'UI/UX Designer', description: 'Design clean and usable interfaces.', location: 'Plovdiv' },
    { title: 'Data Analyst', description: 'Analyze and visualize business data.', location: 'Varna' },
    { title: 'DevOps Engineer', description: 'Manage CI/CD pipelines and deployments.', location: 'Remote' },
    { title: 'QA Tester', description: 'Test web and mobile applications.', location: 'Burgas' },
    { title: 'Mobile Developer', description: 'Develop apps using Flutter.', location: 'Remote' },
    { title: 'Project Manager', description: 'Lead software development teams.', location: 'Sofia' }
  ];

  get filteredJobs() {
    return this.jobs.filter(job =>
      job.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      job.location.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }

  get paginatedJobs() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.filteredJobs.slice(start, start + this.pageSize);
  }

  get totalPages() {
    return Math.ceil(this.filteredJobs.length / this.pageSize);
  }

  changePage(direction: 'prev' | 'next') {
    if (direction === 'prev' && this.currentPage > 1) {
      this.currentPage--;
    } else if (direction === 'next' && this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }
}

