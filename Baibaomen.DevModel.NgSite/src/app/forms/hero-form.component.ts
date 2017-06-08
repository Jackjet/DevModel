import { Component } from '@angular/core';
import { Hero4Form } from './hero';

@Component({
    selector: 'hero-form',
    templateUrl:'hero-form.component.html'
})
export class HeroFormComponent{
    powers = ['Really Smart', 'Super Flexible', 'Super Hot', 'Weather Changer'];
    
    hero = new Hero4Form(18, 'Dr IQ', this.powers[0], 'Chuck Overstreet');

    submitted = false;

    onsubmit() {
    this.submitted = true
    }

    get diagnostic() { return JSON.stringify(this.hero); }
}