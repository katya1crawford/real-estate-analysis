import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    selector: 'app-limited-partnership',
    templateUrl: './limited-partnership.component.html',
})
export class LimitedPartnershipComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Limited Partnership`);
    }
}
