import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LandingComponent } from './features/landing/landing.component';

const routes: Routes = [
  {
    path: 'landing',
    component: LandingComponent,
    data: { title: 'Landing Page' }
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
