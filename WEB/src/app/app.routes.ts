import { Routes } from '@angular/router';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { RegisterComponent } from './components/authentication/register/register.component';
import { LoginComponent } from './components/authentication/login/login.component';
import { JobListComponent } from './components/job-list/job-list.component';
import { JobCreateComponent } from './components/job-create/job-create.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';

export const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'jobs', component: JobListComponent },
  { path: 'jobs/create', component: JobCreateComponent },
  { path: 'profile', component: UserProfileComponent }
];
