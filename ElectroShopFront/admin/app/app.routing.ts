import { Routes, RouterModule } from '@angular/router';
import { PagesComponent } from './pages/pages.component';

const appRoutes: Routes = [
  {
    path: '',
    redirectTo: 'pages-shop/index',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'pages-shop/index'
  }
];

export const routing = RouterModule.forRoot(appRoutes);
