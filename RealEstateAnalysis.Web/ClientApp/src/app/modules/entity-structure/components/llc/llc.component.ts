import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    selector: 'app-llc',
    templateUrl: './llc.component.html'
})
export class LlcComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: LLC`);
    }
}
