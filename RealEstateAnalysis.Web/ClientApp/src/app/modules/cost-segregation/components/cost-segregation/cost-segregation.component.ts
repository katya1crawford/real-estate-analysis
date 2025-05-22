import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    templateUrl: './cost-segregation.component.html',
})
export class CostSegregationComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Cost Segregation`);
    }
}
