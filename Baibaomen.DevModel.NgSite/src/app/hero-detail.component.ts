import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Location } from '@angular/common';
import { Hero } from './hero';
import { HeroService } from './hero.service';
import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css'],
  providers: [ HeroService]
})
export class HeroDetailComponent implements OnInit {
    hero: Hero;

    constructor(private router: ActivatedRoute, private heroService: HeroService, private location: Location) { }

    ngOnInit() {
        this.router.params.subscribe(p => this.heroService.getHero(parseInt(p['id'])).then(hero => this.hero = hero));
    }

    goBack():void {
        this.location.back();
    }
}