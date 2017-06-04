import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeroesComponent } from './heroes.component';
import { HeroDetailComponent } from './hero-detail.component';
import { DashboardComponent } from './dashboard.component';

@NgModule({
    declarations: [
        AppComponent,
        HeroDetailComponent,
        HeroesComponent,
        DashboardComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        RouterModule.forRoot([{
            path: 'heroes',
            component: HeroesComponent
        },
        {
            path: 'dashboard',
            component: DashboardComponent
            },
        {
            path: '',
            redirectTo: '/dashboard',
            pathMatch:'full'
        }
        ], { useHash:true })
    ],
    providers: [
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
