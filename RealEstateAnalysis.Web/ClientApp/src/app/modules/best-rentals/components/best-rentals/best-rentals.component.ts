import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    templateUrl: './best-rentals.component.html',
})
export class BestRentalsComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Which Properties Make the Best Rentals?`);
    }
}
