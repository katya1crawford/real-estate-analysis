import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    templateUrl: './submitting-your-offer.component.html',
})
export class SubmittingYourOfferComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Submitting Your Offer`);
    }
}
