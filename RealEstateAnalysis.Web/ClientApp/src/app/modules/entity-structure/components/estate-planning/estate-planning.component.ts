import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    templateUrl: './estate-planning.component.html',
})
export class EstatePlanningComponent {
    constructor(private titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Estate Planning`);
    }
}
