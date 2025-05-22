import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from '../../../../app.config';

@Component({
    templateUrl: './terms.component.html'
})
export class TermsComponent {
    public appName = '';

    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Terms and Conditions`);
        this.appName = appConfig.businessName;
    }
}
