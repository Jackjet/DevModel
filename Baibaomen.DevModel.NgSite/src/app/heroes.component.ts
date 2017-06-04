import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Hero } from './hero';
import { HeroService } from './hero.service';

@Component({
  selector: 'my-heroes',
  templateUrl: './heroes.component.html',
  styleUrls: ['./heroes.component.css'],
  providers: [HeroService]
})
export class HeroesComponent implements OnInit {
    title = 'Tour ofa Heroes';
    selectedHero: Hero;
    heroes: Hero[];

    constructor(private router: Router, private heroService: HeroService) {
    }

    ngOnInit(): void {
        this.getHeroes();
    }

    getHeroes() {
        this.heroService.getHeroes().then(heroes => this.heroes = heroes);
    }

    onSelect(hero: Hero) {
        this.selectedHero = hero;
    }

    gotoDetail(hero: Hero) {
        this.router.navigate(['/detail', this.selectedHero.id]);
    }
}