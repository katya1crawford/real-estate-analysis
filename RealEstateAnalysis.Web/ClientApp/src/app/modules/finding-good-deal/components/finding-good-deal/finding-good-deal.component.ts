import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    templateUrl: './finding-good-deal.component.html',
})
export class FindingGoodDealComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Finding a Good Deal`);
    }
}
