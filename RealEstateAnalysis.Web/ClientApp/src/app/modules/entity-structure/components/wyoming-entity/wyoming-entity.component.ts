import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { appConfig } from 'src/app/app.config';

@Component({
    selector: 'app-wyoming-entity',
    templateUrl: './wyoming-entity.component.html'
})
export class WyomingEntityComponent {
    constructor(titleService: Title) {
        titleService.setTitle(`${appConfig.businessName}: Taxation`);
    }
}
