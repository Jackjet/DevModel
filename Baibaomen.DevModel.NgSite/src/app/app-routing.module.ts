import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { HeroDetailComponent } from './hero-detail.component';
import { HeroesComponent } from './heroes.component';
import { HeroFormComponent } from './forms/hero-form.component';

const routes: Routes = [{
    path: 'dashboard',
    component: DashboardComponent
},
{
    path: 'detail/:id',
    component: HeroDetailComponent
},
{
    path: 'heroes',
    component: HeroesComponent
},
{
    path: 'hero',
    component: HeroFormComponent
},
{
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full'
}];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash:true })],
    exports: [RouterModule]
})
export class AppRoutingModule { }