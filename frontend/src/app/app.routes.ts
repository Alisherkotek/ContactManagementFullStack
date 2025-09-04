import { Routes } from '@angular/router';
import { ContactListComponent } from './components/contact-list/contact-list';

export const routes: Routes = [
  { path: '', redirectTo: '/contacts', pathMatch: 'full' },
  { path: 'contacts', component: ContactListComponent },
  { path: '**', redirectTo: '/contacts' }
];
