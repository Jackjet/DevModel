import { Injectable } from '@angular/core';
import { Hero } from './hero';
import { HEROES } from './hero-mock';

@Injectable()
export class HeroService {
    getHeroes(): Promise<Hero[]> {
        //return Promise.resolve(HEROES);
        return new Promise(resolve => setTimeout(() => resolve(HEROES), 500));
    }

    getHero(id: number): Promise<Hero> {
        return new Promise(resolve => setTimeout(() => resolve(HEROES.find(h => h.id === id)), 500));
    }
}