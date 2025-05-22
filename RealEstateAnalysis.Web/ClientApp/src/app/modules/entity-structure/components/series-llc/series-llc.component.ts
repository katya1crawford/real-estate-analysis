import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    selector: 'app-series-llc',
    templateUrl: './series-llc.component.html'
})
export class SeriesLlcComponent {
    constructor(titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Series LLC`);
    }
}
