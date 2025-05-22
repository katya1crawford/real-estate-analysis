import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from '../../../../app.config';

@Component({
    templateUrl: './privacy.component.html'
})
export class PrivacyComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Privacy policy`);
    }
}
